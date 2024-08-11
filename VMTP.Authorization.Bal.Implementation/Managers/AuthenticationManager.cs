using MediatR;
using Microsoft.Extensions.Logging;
using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication;
using VMTP.Authorization.Bal.Abstractions.Managers.Authentication.Requests;
using VMTP.Authorization.Bal.Abstractions.Providers.Authentication.Requests;
using VMTP.Authorization.Bal.Implementation.Handlers.Commands.Authentication;
using VMTP.Authorization.Bal.Implementation.Handlers.Commands.Entry;
using VMTP.Authorization.Bal.Implementation.Handlers.Queries.Authorization;
using VMTP.Authorization.Domain.Utilities;
using VMTP.Dal.Abstractions;

namespace VMTP.Authorization.Bal.Implementation.Managers;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IMediator _mediator;
    private readonly ITransactionContext _transactionContext;
    private readonly ILogger<AuthenticationManager> _logger;

    public AuthenticationManager(IMediator mediator, ILogger<AuthenticationManager> logger,
        ITransactionContext transactionContext)
    {
        _mediator = mediator;
        _logger = logger;
        _transactionContext = transactionContext;
    }

    public async Task<AuthenticationDeepedModel> CreateAuthenticationWithEntryAsync(CreateAuthenticationRequest request,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _transactionContext.BeginTransactionAsync(cancellationToken);

        var authentication = await _mediator.Send(
            new AddAuthenticationCommand(request.Login, HashUtil.ComputeHash(request.Password)),
            cancellationToken);

        var entry = await _mediator.Send(new AddEntryCommand(authentication.Id, request.Ip, request.Device),
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new AuthenticationDeepedModel(authentication.Id, entry.Id, authentication.Login, entry.Ip, entry.Device);
    }

    public async Task<AuthenticationModel?> SearchAuthentiactionAsync(string login, CancellationToken cancellationToken)
    {
        var authetnication = await _mediator.Send(new SearchAuthorizationByLoginQuery(login), cancellationToken);
        return authetnication == null ? null : new AuthenticationModel(authetnication.Id, authetnication.Login);
    }

    public async Task<AuthenticationModel> GetAuthenticationAndValidateAsync(
        GetAuthenticationAndValidateRequest request,
        CancellationToken cancellationToken)
    {
        var authentication =
            await _mediator.Send(new SearchAuthorizationByLoginQuery(request.Login), cancellationToken);

        if (authentication == null)
            throw new UserIsNotRegisteredException();

        if (authentication.Password == HashUtil.ComputeHash(request.Password))
            throw new WrongPasswordException();

        return new AuthenticationModel(authentication.Id, authentication.Login);
    }
}