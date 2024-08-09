using Microsoft.EntityFrameworkCore;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Dal.Implementation.Contexts;

public class CodeWriteContext : DbContext, ICodeWriteContext
{
    public DbSet<Domain.Entities.Code> Codes => Set<Domain.Entities.Code>();

    public CodeWriteContext(DbContextOptions<CodeWriteContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeWriteContext).Assembly);
    }
}