using VMTP.Authorization.Application.Models.DTOs;

namespace VMTP.Authorization.Dal.Abstractions.Storage;

public interface IEntryStorage
{
    Task<EntryDTO> AddAsync(Guid authenticationId, string ip, string device, CancellationToken cancellationToken);
    Task UpdateTokenAsync(Guid entryId, string token, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<EntryDTO>> FindByAuthenticationIdAsync(Guid authenticationId,
        CancellationToken cancellationToken);

    Task<EntryDTO?> FindByDeviceAndAuthenticationIdAsync(Guid authenticationId,
        string device, CancellationToken cancellationToken);
}