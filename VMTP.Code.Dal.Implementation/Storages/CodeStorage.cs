using Microsoft.EntityFrameworkCore;
using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Dal.Abstractions.Contexts;
using VMTP.Code.Dal.Abstractions.Storages;
using VMTP.Code.Dal.Abstractions.Storages.Requests;

namespace VMTP.Code.Dal.Implementation.Storages;

public class CodeStorage : ICodeStorage
{
    private readonly ICodeContext _context;

    public CodeStorage(ICodeContext context)
    {
        _context = context;
    }

    public async Task<CodeDTO> AddAsync(AddCodeRequest request, CancellationToken cancellationToken)
    {
        var entry = new Domain.Entities.Code()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            CodeType = request.CodeType,
            Value = request.Value,
            ExpirationTime = request.AbsoluteExpiration.DateTime,
        };
        
        await _context.Codes.AddAsync(entry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CodeDTO()
        {
            Id = entry.Id,
            Email = entry.Email,
            CodeType = entry.CodeType,
            Value = request.Value,
            ExpirationTime = entry.ExpirationTime
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _context.Codes
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<CodeDTO?> FindCodeByValueAsync(string value, CancellationToken cancellationToken)
    {
        return await _context.Codes
            .Where(x => x.Value == value)
            .Select(x => new CodeDTO()
            {
                Id = x.Id,
                Email = x.Email,
                CodeType = x.CodeType,
                Value = x.Value,
                ExpirationTime = x.ExpirationTime,
                AuthorizationId = x.AuthorizationId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CodeDTO>> FindCodesByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Codes
            .Where(x => x.Email == email)
            .Select(x => new CodeDTO()
            {
                Id = x.Id,
                Email = x.Email,
                CodeType = x.CodeType,
                Value = x.Value,
                ExpirationTime = x.ExpirationTime,
                AuthorizationId = x.AuthorizationId
            }).ToArrayAsync(cancellationToken);
    }
}