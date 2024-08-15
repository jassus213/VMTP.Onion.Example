using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Application.Models.Exceptions;
using VMTP.Code.Bal.Abstractions.Managers;
using VMTP.Code.Bal.Abstractions.Managers.Requests;
using VMTP.Code.Bal.Abstractions.Providers.Requests;
using VMTP.Code.Dal.Abstractions.Storages;
using VMTP.Code.Dal.Abstractions.Storages.Requests;
using VMTP.Code.Domain.Utilities;

namespace VMTP.Code.Bal.Implementation.Managers;

public class CodeManager : ICodeManager
{
    private readonly ICodeStorage _codeStorage;

    public CodeManager(ICodeStorage codeStorage)
    {
        _codeStorage = codeStorage;
    }

    public async Task<CodeDTO> CreateAsync(CreateCodeRequest request, CancellationToken cancellationToken)
    {
        var codes = await _codeStorage.FindCodesByEmailAsync(request.Email, cancellationToken);
        if (codes.Any(x => x.CodeType == request.CodeType))
            throw new CodeAlreadyExistException();

        var value = CodeUtil.Generate();
        var code = await _codeStorage.AddAsync(
            new AddCodeRequest(request.Email, request.CodeType, value, DateTimeOffset.UtcNow.AddMinutes(15)),
            cancellationToken);

        return code;
    }

    public async Task DeleteAsync(string value, CancellationToken cancellationToken)
    {
        var code = await _codeStorage.FindCodeByValueAsync(value, cancellationToken);
        if (code == null)
            return;

        await _codeStorage.DeleteAsync(code.Id, cancellationToken);
    }

    public async Task ValidateCodeOrThrowAsync(ValidateCodeOrThrowRequest request, CancellationToken cancellationToken)
    {
        var code = await _codeStorage.FindCodeByValueAsync(request.Value, cancellationToken);
        if (code == null)
            throw new CodeNotExistException();

        if (code.CodeType == request.CodeType && code.Email == request.Email)
            return;

        throw new CodeInvalidException();
    }
}