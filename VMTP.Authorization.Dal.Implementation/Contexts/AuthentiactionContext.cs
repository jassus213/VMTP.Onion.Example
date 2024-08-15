using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using VMTP.Authorization.Dal.Abstractions.Contexts;
using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Implementation.Contexts;

public class AuthentiactionContext : DbContext, IAuthenticationContext
{
    public DbSet<Authentication> Authentications => Set<Authentication>();
    public DbSet<Entry> Entries => Set<Entry>();

    public AuthentiactionContext(DbContextOptions<AuthentiactionContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthentiactionContext).Assembly);
    }

    /// <inheritdoc cref="DatabaseFacade.BeginTransactionAsync"/>
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await Database.BeginTransactionAsync(cancellationToken);
}