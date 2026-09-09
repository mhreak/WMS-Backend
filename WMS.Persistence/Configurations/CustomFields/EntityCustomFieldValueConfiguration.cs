using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.CustomFields;

namespace WMS.Persistence.Configurations.CustomFields;

public class EntityCustomFieldValueConfiguration : IEntityTypeConfiguration<EntityCustomFieldValue>
{
    public void Configure(EntityTypeBuilder<EntityCustomFieldValue> builder)
    {
        builder.ToTable("EntityCustomField_Entity");

        // Composite Key
        builder.HasKey(x => new { x.EntityCustomFieldId, x.EntityId });

        builder.Property(x => x.Value)
            .HasMaxLength(1000);

        builder.HasOne(x => x.EntityCustomField)
            .WithMany(f => f.Values)
            .HasForeignKey(x => x.EntityCustomFieldId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.EntityId);
        builder.HasIndex(x => x.EntityCustomFieldId);
    }
}