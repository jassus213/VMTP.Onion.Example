using MediatR;
using Microsoft.EntityFrameworkCore;
using VMTP.Authorization.Application.Models.DTOs;
using VMTP.Authorization.Dal.Abstractions.Contexts;

namespace VMTP.Authorization.Bal.Implementation.Handlers.Queries.Entry;

public record SearchEntryByAuthorizationIdQuery(Guid AuthorizationId) : IRequest<EntryDTO?>;

public class SearchEntryByAuthorizationIdQueryHandler : IRequestHandler<SearchEntryByAuthorizationIdQuery, EntryDTO?>
{
    private readonly IAuthenticationReadContext _context;

    public SearchEntryByAuthorizationIdQueryHandler(IAuthenticationReadContext context)
    {
        _context = context;
    }

    public async Task<EntryDTO?> Handle(SearchEntryByAuthorizationIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Entries
            .Where(x => x.AuthenticationId == request.AuthorizationId)
            .Select(x => new EntryDTO()
            {
                Id = x.Id,
                AuthenticationId = x.AuthenticationId,
                Device = x.Device,
                Ip = x.Ip,
                Token = x.Token
            }).FirstOrDefaultAsync(cancellationToken);
    }
}