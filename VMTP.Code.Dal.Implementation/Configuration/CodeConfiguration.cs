using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VMTP.Code.Dal.Implementation.Configuration;

public class CodeConfiguration : IEntityTypeConfiguration<Domain.Entities.Code>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Code> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email);
        builder.Property(x => x.Value);
        builder.Property(x => x.ExpirationTime)
            .IsRequired();
        
        builder.Property(x => x.CodeType)
            .IsRequired();

        builder.HasIndex(x => x.AuthorizationId);
    }
}