using VMTP.Authorization.Bal.Abstractions.Managers.Requests;

namespace VMTP.Authorization.Bal.Abstractions.Managers;

public interface IRegisterManager
{
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    Task RegisterChallengeAsync(RegisterChallengeRequest request, CancellationToken cancellationToken);
}