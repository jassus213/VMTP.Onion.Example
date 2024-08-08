using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using VMTP.Authorization.Dal.Abstractions.Contexts;
using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Implementation.Contexts;

public class AuthentiactionWriteContext : DbContext, IAuthenticationWriteContext
{
    public DbSet<Authentication> Authentications => Set<Authentication>();
    public DbSet<Entry> Entries => Set<Entry>();

    public AuthentiactionWriteContext(DbContextOptions<AuthentiactionWriteContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthentiactionWriteContext).Assembly);
    }

    /// <inheritdoc cref="DatabaseFacade.BeginTransactionAsync"/>
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await Database.BeginTransactionAsync(cancellationToken);
}