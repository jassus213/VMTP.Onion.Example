using Microsoft.EntityFrameworkCore;
using VMTP.Authorization.Dal.Abstractions.Contexts;
using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Implementation.Contexts;

public class AuthentiactionReadContext : DbContext, IAuthentiactionReadContext
{
    public IQueryable<Authentication> Authentications => Set<Authentication>()
        .AsNoTracking();

    public IQueryable<Entry> Entries => Set<Entry>()
        .AsNoTracking();

    public AuthentiactionReadContext(DbContextOptions<AuthentiactionReadContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthentiactionReadContext).Assembly);
    }
}