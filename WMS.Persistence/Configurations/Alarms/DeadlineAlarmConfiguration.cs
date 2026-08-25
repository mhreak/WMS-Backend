using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Alarms;

namespace WMS.Persistence.Configurations.Alarms;

public class DeadlineAlarmConfiguration : IEntityTypeConfiguration<DeadlineAlarm>
{
    public void Configure(EntityTypeBuilder<DeadlineAlarm> builder)
    {
        builder.ToTable("DeadlineAlarm");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EntityType).IsRequired().HasConversion<int>();
        builder.Property(x => x.DaysBeforeDeadline).IsRequired();

        builder.HasIndex(x => new { x.ContractId, x.StepId });
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
    }
}