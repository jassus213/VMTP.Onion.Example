using Microsoft.EntityFrameworkCore.Storage;
using VMTP.Authorization.Dal.Abstractions.Contexts;
using VMTP.Authorization.Dal.Abstractions.Storage;

namespace VMTP.Authorization.Dal.Implementation.Storages;

public class AuthenticationAndEntryUnitOfWork : IAuthenticationAndEntryUnitOfWork, IDisposable
{
    public IAuthenticationStorage AuthenticationStorage
    {
        get
        {
            if (_contextTransaction == null)
                throw new InvalidOperationException("Transaction is not Started");

            return _authenticationStorage;
        }
    }

    public IEntryStorage EntryStorage
    {
        get
        {
            if (_contextTransaction == null)
                throw new InvalidOperationException("Transaction is not Started");

            return _entryStorage;
        }
    }

    private readonly IAuthenticationStorage _authenticationStorage;
    private readonly IEntryStorage _entryStorage;

    private readonly IAuthenticationContext _context;
    private IDbContextTransaction? _contextTransaction;

    public AuthenticationAndEntryUnitOfWork(IAuthenticationStorage authenticationStorage, IEntryStorage entryStorage,
        IAuthenticationContext context)
    {
        _authenticationStorage = authenticationStorage;
        _entryStorage = entryStorage;
        _context = context;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _contextTransaction = await _context.BeginTransactionAsync(cancellationToken);
    }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_contextTransaction == null)
                throw new InvalidOperationException("Transaction is not Started");

            await _contextTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            if (_contextTransaction != null)
                await _contextTransaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}