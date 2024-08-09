using MediatR;
using VMTP.Authorization.Application.Models.DTOs;
using VMTP.Authorization.Dal.Abstractions.Contexts;

namespace VMTP.Authorization.Bal.Implementation.Handlers.Commands.Entry;

public record AddEntryCommand(Guid AuthenticationId, string Ip, string Device) : IRequest<EntryDTO>;

public class AddEntryCommandHandler : IRequestHandler<AddEntryCommand, EntryDTO>
{
    private readonly IAuthenticationWriteContext _context;

    public AddEntryCommandHandler(IAuthenticationWriteContext context)
    {
        _context = context;
    }

    public async Task<EntryDTO> Handle(AddEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = new Domain.Entities.Entry()
        {
            Id = Guid.NewGuid(),
            AuthenticationId = request.AuthenticationId,
            Device = request.Device,
            Ip = request.Ip,
            IsTrusted = false
        };

        await _context.Entries.AddAsync(entry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new EntryDTO()
        {
            Id = entry.Id,
            AuthenticationId = entry.AuthenticationId,
            Device = entry.Device,
            Ip = entry.Ip,
            IsTrusted = entry.IsTrusted
        };
    }
}