using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.CustomFields;

namespace WMS.Persistence.Configurations.CustomFields;

public class EntityCustomFieldConfiguration : IEntityTypeConfiguration<EntityCustomField>
{
    public void Configure(EntityTypeBuilder<EntityCustomField> builder)
    {
        builder.ToTable("EntityCustomField");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EntityType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.FieldName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.FieldType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Config)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.IsRequired)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(x => x.EntityType);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new { x.EntityType, x.FieldName });
    }
}