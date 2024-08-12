using VMTP.API.Sagas.Requests;
using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry.Requests;
using VMTP.Authorization.Bal.Abstractions.Managers.Token;
using VMTP.Authorization.Bal.Abstractions.Managers.Token.Requests;
using VMTP.Authorization.Bal.Abstractions.Providers.Authentication;
using VMTP.Authorization.Bal.Abstractions.Providers.Authentication.Requests;
using VMTP.Authorization.Bal.Abstractions.Services.Responses;
using VMTP.Code.Application.Models.Enums;
using VMTP.Code.Bal.Abstractions.Managers;
using VMTP.Code.Bal.Abstractions.Managers.Requests;
using VMTP.Notification.Bal.Abstractions.Managers;
using VMTP.Notification.Bal.Abstractions.Managers.Requests;

namespace VMTP.API.Sagas;

public class LoginSaga
{
    private readonly ILogger<LoginSaga> _logger;
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IEntryManager _entryManager;
    private readonly ITokenManager _tokenManager;
    private readonly ICodeManager _codeManager;
    private readonly INotificationManager _notificationManager;

    public LoginSaga(ILogger<LoginSaga> logger, IAuthenticationProvider authenticationProvider,
        IEntryManager entryManager, ITokenManager tokenManager, ICodeManager codeManager,
        INotificationManager notificationManager)
    {
        _logger = logger;
        _authenticationProvider = authenticationProvider;
        _entryManager = entryManager;
        _tokenManager = tokenManager;
        _codeManager = codeManager;
        _notificationManager = notificationManager;
    }

    public async Task<Tokens> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authentication =
            await _authenticationProvider.GetAuthenticationAndValidateAsync(
                new GetAuthenticationAndValidateRequest(request.Login, request.Password),
                cancellationToken);

        try
        {
            var entry = await _entryManager.GetOrCreateEntryAndValidateAsync(new GetOrCreateEntryAndValidateRequest(
                    authentication.Id, request.Device,
                    request.Ip),
                cancellationToken);

            return await _tokenManager.CreatePair(
                new CreatePairRequest(authentication.Id, entry.Id, authentication.Login, entry.Ip), cancellationToken);
        }
        catch (EntryIsNotTrustedException e)
        {
            _logger.LogWarning(e, "Occurred not trusted entry for account: {Login}", authentication.Login);
            var code = await _codeManager.CreateAsync(new CreateCodeRequest(request.Login, CodeType.SuspiciousEntry),
                cancellationToken);

            await _notificationManager.CreateSuspiciousEntryNotificationAsync(
                new CreateNotificationWithCodeRequest(request.Login, code.Value),
                cancellationToken);

            throw;
        }
    }
}