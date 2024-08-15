using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Providers.Authentication.Requests;

namespace VMTP.Authorization.Bal.Abstractions.Providers.Authentication;

public interface IAuthenticationProvider
{
    Task<AuthenticationModel?> SearchAuthenticationAsync(string login, CancellationToken cancellationToken);

    Task<AuthenticationModel> GetAuthenticationAndValidateAsync(GetAuthenticationAndValidateRequest request,
        CancellationToken cancellationToken);
}