using MediatR;
using VMTP.Authorization.Application.Exceptions;
using VMTP.Authorization.Application.Models.Models;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry;
using VMTP.Authorization.Bal.Abstractions.Managers.Entry.Requests;
using VMTP.Authorization.Bal.Implementation.Handlers.Commands.Entry;
using VMTP.Authorization.Bal.Implementation.Handlers.Queries.Entry;

namespace VMTP.Authorization.Bal.Implementation.Managers;

public class EntryManager : IEntryManager
{
    private readonly IMediator _mediator;

    public EntryManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<EntryModel> GetOrCreateEntryAndValidateAsync(GetOrCreateEntryAndValidateRequest request,
        CancellationToken cancellationToken)
    {
        var entry = await _mediator.Send(new SearchEntryByAuthorizationIdQuery(request.AuthenticationId), cancellationToken);
        if (entry == null)
        {
            await _mediator.Send(new AddEntryCommand(request.AuthenticationId, request.Ip, request.Device), cancellationToken);
            throw new EntryIsNotTrustedException();
        }

        return entry.IsTrusted
            ? new EntryModel(entry.Id, entry.AuthenticationId, entry.Device, entry.Ip, entry.IsTrusted)
            : throw new EntryIsNotTrustedException();
    }
}