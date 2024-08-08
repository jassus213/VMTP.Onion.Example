using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VMTP.Authorization.Domain.Entities;

namespace VMTP.Authorization.Dal.Implementation.Configuration;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Device)
            .IsRequired();

        builder.Property(x => x.Ip)
            .IsRequired();
        
        builder.HasIndex(x => x.Token);

        builder.HasOne(x => x.Authentication)
            .WithMany(x => x.Entries)
            .HasForeignKey(x => x.AuthenticationId)
            .IsRequired();
    }
}