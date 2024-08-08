using VMTP.Authorization.Application.Models.DTOs;

namespace VMTP.Authorization.Bal.Abstractions.Providers;

public interface IAuthenticationProvider
{
    Task<AuthenticationDTO> GetAuthentiactionAsync(string email, CancellationToken cancellationToken);
}