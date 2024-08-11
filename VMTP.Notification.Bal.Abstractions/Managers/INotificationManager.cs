using VMTP.Notification.Bal.Abstractions.Managers.Requests;

namespace VMTP.Notification.Bal.Abstractions.Managers;

public interface INotificationManager
{
    Task CreateAccountConfirmationNotificationAsync(CreateNotificationWithCodeRequest request,
        CancellationToken cancellationToken);

    Task CreateSuspiciousEntryNotificationAsync(CreateNotificationWithCodeRequest request,
        CancellationToken cancellationToken);
}