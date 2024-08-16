using Microsoft.EntityFrameworkCore;
using VMTP.Authorization.Application.Models.DTOs;
using VMTP.Authorization.Dal.Abstractions.Contexts;
using VMTP.Authorization.Dal.Abstractions.Storage;

namespace VMTP.Authorization.Dal.Implementation.Storages;

public class AuthenticationStorage : IAuthenticationStorage
{
    private readonly IAuthenticationContext _context;

    public AuthenticationStorage(IAuthenticationContext context)
    {
        _context = context;
    }

    public async Task<AuthenticationDTO> AddAsync(string login, string password, CancellationToken cancellationToken)
    {
        var authentication = new Domain.Entities.Authentication()
        {
            Id = Guid.NewGuid(),
            Login = login,
            Password = password
        };

        await _context.Authentications.AddAsync(authentication, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthenticationDTO()
        {
            Id = authentication.Id,
            Login = authentication.Login,
            Password = authentication.Password
        };
    }

    public async Task<AuthenticationDTO?> FindByLoginAsync(string login, CancellationToken cancellationToken)
    {
        return await _context.Authentications
            .Where(x => x.Login == login)
            .Select(x => new AuthenticationDTO()
            {
                Id = x.Id,
                Login = x.Login,
                Password = x.Password
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}