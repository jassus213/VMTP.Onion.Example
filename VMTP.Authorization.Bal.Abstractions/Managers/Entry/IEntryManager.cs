using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry.Requests;

namespace VMTP.Authorization.Bal.Abstractions.Managers.Entry;

public interface IEntryManager
{
    Task<EntryModel> GetOrCreateEntryAndValidateAsync(GetOrCreateEntryAndValidateRequest request, CancellationToken cancellationToken);
}