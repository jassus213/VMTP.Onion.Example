using MediatR;
using Microsoft.EntityFrameworkCore;
using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Application.Models.Enums;
using VMTP.Code.Application.Models.Exceptions;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Bal.Implementation.Handlers.Commands;

public record AddCodeIfNotExistCommand(string Email, CodeType CodeType, string Value, DateTimeOffset AbsoluteExpiration) : IRequest<CodeDTO>;

public class AddCodeIfNotExistCommandHandler : IRequestHandler<AddCodeIfNotExistCommand, CodeDTO>
{
    private readonly ICodeWriteContext _context;

    public AddCodeIfNotExistCommandHandler(ICodeWriteContext context)
    {
        _context = context;
    }

    public async Task<CodeDTO> Handle(AddCodeIfNotExistCommand request, CancellationToken cancellationToken)
    {
        var code = await _context.Codes
            .Where(x => x.Email == request.Email && 
                        x.CodeType == request.CodeType && 
                        x.ExpirationTime < DateTimeOffset.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);

        if (code != null)
            throw new CodeAlreadyExistException();

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
}