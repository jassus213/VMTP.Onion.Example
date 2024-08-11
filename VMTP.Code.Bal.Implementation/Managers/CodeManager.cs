using MediatR;
using VMTP.Code.Application.Models.DTOs;
using VMTP.Code.Application.Models.Exceptions;
using VMTP.Code.Bal.Abstractions.Managers;
using VMTP.Code.Bal.Abstractions.Managers.Requests;
using VMTP.Code.Bal.Abstractions.Providers.Requests;
using VMTP.Code.Bal.Implementation.Handlers.Commands;
using VMTP.Code.Bal.Implementation.Handlers.Query;
using VMTP.Code.Domain.Utilities;

namespace VMTP.Code.Bal.Implementation.Managers;

public class CodeManager : ICodeManager
{
    private readonly IMediator _mediatr;

    public CodeManager(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    public async Task<CodeDTO> CreateAsync(CreateCodeRequest request, CancellationToken cancellationToken)
    {
        var userCodes = await _mediatr.Send(new SearchCodesByEmailQuery(request.Email), cancellationToken);
        if (userCodes.Any(x => x.CodeType == request.CodeType))
            throw new CodeAlreadyExistException();
        
        var value = CodeUtil.Generate();
        var code = await _mediatr.Send(new AddCodeIfNotExistCommand(request.Email, request.CodeType, value,
            DateTimeOffset.UtcNow.AddMinutes(15)), cancellationToken);

        return code;
    }

    public async Task DeleteAsync(string value, CancellationToken cancellationToken)
    {
        var code = await _mediatr.Send(new SearchCodeByValueQuery(value), cancellationToken);
        if (code == null)
            return;

        await _mediatr.Send(new DeleteCodeCommand(code.Id), cancellationToken);
    }

    public async Task ValidateCodeOrThrowAsync(ValidateCodeOrThrowRequest request, CancellationToken cancellationToken)
    {
        var code = await _mediatr.Send(new SearchCodeByValueQuery(request.Value), cancellationToken);
        if (code == null)
            throw new CodeNotExistException();
        
        if (code.CodeType == request.CodeType && code.Email == request.Email)
            return;

        throw new CodeInvalidException();
    }
}