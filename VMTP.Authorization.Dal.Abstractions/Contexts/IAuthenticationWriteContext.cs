using Microsoft.EntityFrameworkCore;
using VMTP.Authorization.Domain.Entities;
using VMTP.Dal.Abstractions;

namespace VMTP.Authorization.Dal.Abstractions.Contexts;

public interface IAuthenticationWriteContext : ISavableContext, ITransactionContext
{
    DbSet<Authentication> Authentications { get; }
    DbSet<Entry> Entries { get; }
}