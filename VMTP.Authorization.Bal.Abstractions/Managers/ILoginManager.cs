using VMTP.Authorization.Bal.Abstractions.Managers.Requests;
using VMTP.Authorization.Bal.Abstractions.Services.Responses;

namespace VMTP.Authorization.Bal.Abstractions.Managers;

public interface ILoginManager
{
    Task<Tokens> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}