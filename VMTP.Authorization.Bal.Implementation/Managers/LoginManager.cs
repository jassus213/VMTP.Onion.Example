using MediatR;
using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Bal.Abstractions.Managers;
using VMTP.Authorization.Bal.Abstractions.Managers.Requests;
using VMTP.Authorization.Bal.Abstractions.Services;
using VMTP.Authorization.Bal.Abstractions.Services.Requests;
using VMTP.Authorization.Bal.Abstractions.Services.Responses;
using VMTP.Authorization.Bal.Implementation.Handlers.Commands.Entry;
using VMTP.Authorization.Bal.Implementation.Handlers.Queries.Authorization;
using VMTP.Authorization.Bal.Implementation.Handlers.Queries.Entry;
using VMTP.Authorization.Domain.Utilities;

namespace VMTP.Authorization.Bal.Implementation.Managers;

public class LoginManager : ILoginManager
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public LoginManager(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        _tokenService = tokenService;
    }

    public async Task<Tokens> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authentication =
            await _mediator.Send(new SearchAuthorizationByLoginQuery(request.Login), cancellationToken);

        if (authentication == null)
            throw new UserIsNotRegisteredException();

        if (HashUtil.ComputeHash(request.Password) != authentication.Password)
            throw new WrongPasswordException();

        var entry = await _mediator.Send(new SearchEntryByAuthorizationIdQuery(authentication.Id), cancellationToken);
        if (entry == null)
        {
            await _mediator.Send(new AddEntryCommand(authentication.Id, request.Ip, request.Device), cancellationToken);
            throw new EntryIsNotTrustedException();
        }

        if (!entry.IsTrusted)
        {
            throw new EntryIsNotTrustedException();
        }

        return _tokenService.CreatePair(new IdentityInfo
        {
            AuthorizationId = authentication.Id,
            EntryId = authentication.Entries.First().Id,
            Login = authentication.Login,
            Ip = authentication.Entries.First().Ip
        });
    }
}