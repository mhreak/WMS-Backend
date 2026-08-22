// WMS.Persistence/Configurations/Labels/EntityLabelConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Labels;

namespace WMS.Persistence.Configurations.Labels;

public class EntityLabelConfiguration : IEntityTypeConfiguration<EntityLabel>
{
    public void Configure(EntityTypeBuilder<EntityLabel> builder)
    {
        builder.ToTable("EntityLabel");

        // Composite Key
        builder.HasKey(x => new { x.LabelId, x.EntityId });

        builder.HasOne(x => x.Label)
            .WithMany(l => l.EntityLabels)
            .HasForeignKey(x => x.LabelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.LabelId);
        builder.HasIndex(x => x.EntityId);
    }
}