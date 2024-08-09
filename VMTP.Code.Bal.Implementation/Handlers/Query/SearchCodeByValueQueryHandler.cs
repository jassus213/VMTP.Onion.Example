using MediatR;
using Microsoft.EntityFrameworkCore;
using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Bal.Implementation.Handlers.Query;

public record SearchCodeByValueQuery(string Value) : IRequest<CodeDTO?>;

public class SearchCodeByValueQueryHandler : IRequestHandler<SearchCodeByValueQuery, CodeDTO?>
{
    private readonly ICodeReadContext _context;

    public SearchCodeByValueQueryHandler(ICodeReadContext context)
    {
        _context = context;
    }

    public async Task<CodeDTO?> Handle(SearchCodeByValueQuery request, CancellationToken cancellationToken)
    {
        return await _context.Codes
            .Where(x => x.Value == request.Value)
            .Select(x => new CodeDTO()
            {
                Id = x.Id,
                Email = x.Email,
                CodeType = x.CodeType,
                Value = request.Value,
                ExpirationTime = x.ExpirationTime,
                AuthorizationId = x.AuthorizationId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}