// WMS.Persistence/Configurations/Attachments/EntityAttachmentConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Attachments;

namespace WMS.Persistence.Configurations.Attachments;

public class EntityAttachmentConfiguration : IEntityTypeConfiguration<EntityAttachment>
{
    public void Configure(EntityTypeBuilder<EntityAttachment> builder)
    {
        builder.ToTable("EntityAttachment");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.EntityId)
            .IsRequired();

        builder.Property(x => x.AttachmentTypeId)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Extension)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Size)
            .IsRequired();


        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.Property(x => x.ThumbnailFileName).HasMaxLength(255);

        builder.HasOne(x => x.AttachmentType)
            .WithMany(t => t.EntityAttachments)
            .HasForeignKey(x => x.AttachmentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EntityId);
        builder.HasIndex(x => x.AttachmentTypeId);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new { x.EntityId, x.AttachmentTypeId });
    }
}