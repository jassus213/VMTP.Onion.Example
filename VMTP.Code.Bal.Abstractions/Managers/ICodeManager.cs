using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Bal.Abstractions.Managers.Requests;
using VMTP.Code.Bal.Abstractions.Providers;

namespace VMTP.Code.Bal.Abstractions.Managers;

public interface ICodeManager : ICodeProvider
{
    Task<CodeDTO> CreateAsync(CreateCodeRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(string value, CancellationToken cancellationToken);
}