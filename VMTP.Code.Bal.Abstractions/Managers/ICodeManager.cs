using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Bal.Abstractions.Managers.Requests;

namespace VMTP.Code.Bal.Abstractions.Managers;

public interface ICodeManager
{
    Task<CodeDTO> CreateAsync(CreateCodeRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(string value, CancellationToken cancellationToken);
}