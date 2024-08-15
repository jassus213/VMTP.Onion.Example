using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Dal.Abstractions.Storages.Requests;

namespace VMTP.Code.Dal.Abstractions.Storages;

public interface ICodeStorage
{
    Task<CodeDTO> AddAsync(AddCodeRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<CodeDTO?> FindCodeByValueAsync(string value, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CodeDTO>> FindCodesByEmailAsync(string email, CancellationToken cancellationToken);
}