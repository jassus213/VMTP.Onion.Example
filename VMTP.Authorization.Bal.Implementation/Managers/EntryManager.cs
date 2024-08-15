using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry.Requests;
using VMTP.Authorization.Dal.Abstractions.Storage;

namespace VMTP.Authorization.Bal.Implementation.Managers;

public class EntryManager : IEntryManager
{
    private readonly IEntryStorage _entryStorage;

    public EntryManager(IEntryStorage entryStorage)
    {
        _entryStorage = entryStorage;
    }

    public async Task<EntryModel> GetOrCreateEntryAndValidateAsync(GetOrCreateEntryAndValidateRequest request,
        CancellationToken cancellationToken)
    {
        var entry = await _entryStorage.FindByAuthenticationIdAsync(request.AuthenticationId, cancellationToken);
        if (entry == null)
        {
            await _entryStorage.AddAsync(request.AuthenticationId, request.Ip, request.Device, cancellationToken);
            throw new EntryIsNotTrustedException();
        }

        return entry.IsTrusted
            ? new EntryModel(entry.Id, entry.AuthenticationId, entry.Device, entry.Ip, entry.IsTrusted)
            : throw new EntryIsNotTrustedException();
    }
}