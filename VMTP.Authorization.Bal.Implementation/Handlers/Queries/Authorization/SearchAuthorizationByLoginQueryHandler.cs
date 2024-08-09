using MediatR;
using Microsoft.EntityFrameworkCore;
using VMTP.Authorization.Application.Models.DTOs;
using VMTP.Authorization.Dal.Abstractions.Contexts;

namespace VMTP.Authorization.Bal.Implementation.Handlers.Queries.Authorization;

public record SearchAuthorizationByLoginQuery(string Login) : IRequest<AuthenticationDTO?>;
    
public class SearchAuthorizationByLoginQueryHandler : IRequestHandler<SearchAuthorizationByLoginQuery, AuthenticationDTO?>
{
    private readonly IAuthenticationReadContext _context;

    public SearchAuthorizationByLoginQueryHandler(IAuthenticationReadContext context)
    {
        _context = context;
    }

    public async Task<AuthenticationDTO?> Handle(SearchAuthorizationByLoginQuery request,
        CancellationToken cancellationToken)
        => await _context.Authentications
            .Where(x => x.Login == request.Login)
            .Select(x => new AuthenticationDTO()
            {
                Id = x.Id,
                Login = x.Login,
                Password = x.Password
            })
            .FirstOrDefaultAsync(cancellationToken);
}