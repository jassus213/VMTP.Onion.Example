using Microsoft.EntityFrameworkCore;
using VMTP.Code.Dal.Abstractions.Contexts;

namespace VMTP.Code.Dal.Implementation.Contexts;

public class CodeReadContext : DbContext, ICodeReadContext
{
    public IQueryable<Domain.Entities.Code> Codes => Set<Domain.Entities.Code>()
        .AsNoTracking();

    public CodeReadContext(DbContextOptions<CodeReadContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeReadContext).Assembly);
    }
}