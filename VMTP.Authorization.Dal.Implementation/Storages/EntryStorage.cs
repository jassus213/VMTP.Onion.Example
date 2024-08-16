using Microsoft.EntityFrameworkCore;
using VMTP.Authorization.Application.Models.DTOs;
using VMTP.Authorization.Dal.Abstractions.Contexts;
using VMTP.Authorization.Dal.Abstractions.Storage;
using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Implementation.Storages;

public class EntryStorage : IEntryStorage
{
    private readonly IAuthenticationContext _context;

    public EntryStorage(IAuthenticationContext context)
    {
        _context = context;
    }

    public async Task<EntryDTO> AddAsync(Guid authenticationId, string ip, string device,
        CancellationToken cancellationToken)
    {
        var entry = new Entry()
        {
            Id = Guid.NewGuid(),
            AuthenticationId = authenticationId,
            Ip = ip,
            Device = device,
            IsTrusted = false,
        };

        await _context.Entries.AddAsync(entry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new EntryDTO()
        {
            Id = entry.Id,
            AuthenticationId = entry.AuthenticationId,
            Ip = entry.Ip,
            Device = entry.Device,
            IsTrusted = entry.IsTrusted,
        };
    }

    public async Task UpdateTokenAsync(Guid entryId, string token, CancellationToken cancellationToken)
    {
        await _context.Entries
            .Where(x => x.Id == entryId)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.Token, token), cancellationToken);
    }

    public async Task<IReadOnlyCollection<EntryDTO>> FindByAuthenticationIdAsync(Guid authenticationId,
        CancellationToken cancellationToken)
    {
        return await _context.Entries
            .Where(x => x.AuthenticationId == authenticationId)
            .Select(x => new EntryDTO()
            {
                Id = x.Id,
                AuthenticationId = x.AuthenticationId,
                Ip = x.Ip,
                Device = x.Device,
                IsTrusted = x.IsTrusted,
            }).ToArrayAsync(cancellationToken);
    }

    public async Task<EntryDTO?> FindByDeviceAndAuthenticationIdAsync(Guid authenticationId,
        string device, CancellationToken cancellationToken)
    {
        return await _context.Entries
            .Where(x => x.AuthenticationId == authenticationId && x.Device == device)
            .Select(x => new EntryDTO()
            {
                Id = x.Id,
                AuthenticationId = x.AuthenticationId,
                Ip = x.Ip,
                Device = x.Device,
                IsTrusted = x.IsTrusted,
            }).FirstOrDefaultAsync(cancellationToken);
    }
}