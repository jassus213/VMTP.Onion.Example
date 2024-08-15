using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication.Requests;
using VMTP.Authorization.Bal.Abstractions.Providers.Authentication;

namespace VMTP.Authorization.Bal.Abstractions.Managers.Authentication;

public interface IAuthenticationManager : IAuthenticationProvider
{
    Task<AuthenticationDeepModel> CreateAuthenticationWithEntryAsync(CreateAuthenticationRequest request, CancellationToken cancellationToken);
}