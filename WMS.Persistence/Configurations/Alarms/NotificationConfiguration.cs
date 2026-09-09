using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Notifications;

namespace WMS.Persistence.Configurations.Notifications;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notification");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Minutes).IsRequired();
        builder.Property(x => x.EntityType).IsRequired().HasConversion<int>();
        builder.Property(x => x.FieldIndicator).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PeriodicCheck).IsRequired();
        builder.Property(x => x.Config).HasColumnType("nvarchar(max)");

        builder.HasIndex(x => x.EntityType);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
    }
}