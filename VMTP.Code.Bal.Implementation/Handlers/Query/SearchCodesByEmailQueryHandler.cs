using MediatR;
using Microsoft.EntityFrameworkCore;
using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Bal.Implementation.Handlers.Query;

public record SearchCodesByEmailQuery(string Email) : IRequest<IReadOnlyCollection<CodeDTO>>;

public class SearchCodesByEmailQueryHandler : IRequestHandler<SearchCodesByEmailQuery, IReadOnlyCollection<CodeDTO>>
{
    private readonly ICodeReadContext _context;

    public SearchCodesByEmailQueryHandler(ICodeReadContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<CodeDTO>> Handle(SearchCodesByEmailQuery request,
        CancellationToken cancellationToken)
        => await _context.Codes
            .Where(x => x.Email == request.Email)
            .Select(x => new CodeDTO()
            {
                Id = x.Id,
                Email = x.Email,
                CodeType = x.CodeType,
                Value = x.Value,
                ExpirationTime = x.ExpirationTime,
                AuthorizationId = x.AuthorizationId
            })
            .ToArrayAsync(cancellationToken);
}