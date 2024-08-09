using VMTP.Code.Bal.Abstractions.Providers.Requests;

namespace VMTP.Code.Bal.Abstractions.Providers;

public interface ICodeProvider
{
    Task ValidateCodeOrThrowAsync(ValidateCodeOrThrowRequest request, CancellationToken cancellationToken);
}