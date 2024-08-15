using VMTP.Authorization.Application.Models.DTOs;

namespace VMTP.Authorization.Dal.Abstractions.Storage;

public interface IEntryStorage
{
    Task<EntryDTO> AddAsync(Guid authenticationId, string ip, string device, CancellationToken cancellationToken);

    Task<EntryDTO?> FindByAuthenticationIdAsync(Guid authenticationId, CancellationToken cancellationToken);
}