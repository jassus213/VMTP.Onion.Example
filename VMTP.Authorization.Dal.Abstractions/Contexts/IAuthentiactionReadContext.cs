using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Abstractions.Contexts;

public interface IAuthentiactionReadContext
{
    IQueryable<Authentication> Authentications { get; }
    IQueryable<Entry> Entries { get; }
}