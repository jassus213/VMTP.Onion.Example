namespace VMTP.Authorization.Dal.Abstractions.Storage;

public interface IAuthenticationAndEntryUnitOfWork
{
    IAuthenticationStorage AuthenticationStorage { get; }
    IEntryStorage EntryStorage { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CompleteAsync(CancellationToken cancellationToken);
}