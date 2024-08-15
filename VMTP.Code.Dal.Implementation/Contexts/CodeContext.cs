using Microsoft.EntityFrameworkCore;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Dal.Implementation.Contexts;

public class CodeContext : DbContext, ICodeContext
{
    public DbSet<Domain.Entities.Code> Codes => Set<Domain.Entities.Code>();

    public CodeContext(DbContextOptions<CodeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeContext).Assembly);
    }
}