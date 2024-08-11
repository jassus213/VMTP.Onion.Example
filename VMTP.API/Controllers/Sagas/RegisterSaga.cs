using System.Transactions;
using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication.Requests;
using VMTP.Authorization.Bal.Abstractions.Managers.Token;
using VMTP.Authorization.Bal.Abstractions.Managers.Token.Requests;
using VMTP.Authorization.Bal.Abstractions.Services.Responses;
using VMTP.Code.Application.Models.Enums;
using VMTP.Code.Bal.Abstractions.Managers;
using VMTP.Code.Bal.Abstractions.Managers.Requests;
using VMTP.Code.Bal.Abstractions.Providers.Requests;
using VMTP.Notification.Bal.Abstractions.Managers;
using VMTP.Notification.Bal.Abstractions.Managers.Requests;
using RegisterRequest = VMTP.API.Controllers.Sagas.Requests.RegisterRequest;

namespace VMTP.API.Controllers.Sagas;

public class RegisterSaga
{
    private readonly ILogger<RegisterSaga> _logger;
    private readonly IAuthenticationManager _authenticationManager;
    private readonly ITokenManager _tokenManager;
    private readonly ICodeManager _codeManager;
    private readonly INotificationManager _notificationManager;

    public RegisterSaga(ILogger<RegisterSaga> logger, IAuthenticationManager authenticationManager,
        ICodeManager codeManager, INotificationManager notificationManager, ITokenManager tokenManager)
    {
        _logger = logger;
        _authenticationManager = authenticationManager;
        _codeManager = codeManager;
        _notificationManager = notificationManager;
        _tokenManager = tokenManager;
    }

    public async Task RegisterChallengeAsync(string login, CancellationToken cancellationToken)
    {
        var authentication = await _authenticationManager.SearchAuthentiactionAsync(login, cancellationToken);
        if (authentication != null)
            throw new UserAlreadyExistException();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var code = await _codeManager.CreateAsync(new CreateCodeRequest(login, CodeType.AccountConfirmation),
            cancellationToken);

        await _notificationManager.CreateAccountConfirmationNotificationAsync(
            new CreateNotificationWithCodeRequest(login, code.Value),
            cancellationToken);
        
        scope.Complete();
    }

    public async Task<Tokens> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var authentication = await _authenticationManager.SearchAuthentiactionAsync(request.Login, cancellationToken);
        if (authentication != null)
            throw new UserAlreadyExistException();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        await _codeManager.ValidateCodeOrThrowAsync(
            new ValidateCodeOrThrowRequest(request.Login, request.Code, CodeType.AccountConfirmation),
            cancellationToken);
        
        await _codeManager.DeleteAsync(request.Code, cancellationToken);

        var authenticationModel = await _authenticationManager.CreateAuthenticationWithEntryAsync(
            new CreateAuthenticationRequest(request.Login, request.Password, request.Ip, request.Device),
            cancellationToken);

        var tokens = await _tokenManager.CreatePair(
            new CreatePairRequest(authenticationModel.AuthenticationId, authenticationModel.EntryId,
                authenticationModel.Email, authenticationModel.Ip), cancellationToken);
        
        scope.Complete();

        return tokens;
    }
}