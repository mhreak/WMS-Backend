// WMS.Persistence/Configurations/Attachments/AttachmentTypeConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Attachments;

namespace WMS.Persistence.Configurations.Attachments;

public class AttachmentTypeConfiguration : IEntityTypeConfiguration<AttachmentType>
{
    public void Configure(EntityTypeBuilder<AttachmentType> builder)
    {
        builder.ToTable("AttachmentType");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EntityType)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(x => x.EntityType);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new { x.EntityType, x.Title });
    }
}