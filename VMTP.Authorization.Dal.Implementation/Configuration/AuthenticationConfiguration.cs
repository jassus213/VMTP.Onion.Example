using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Implementation.Configuration;

public class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
{
    public void Configure(EntityTypeBuilder<Authentication> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Login)
            .IsUnique();

        builder.Property(x => x.Password)
            .IsRequired();

        builder.HasMany(x => x.Entries)
            .WithOne(x => x.Authentication)
            .HasForeignKey(x => x.AuthenticationId)
            .IsRequired();
    }
}