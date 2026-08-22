// داخل ContractTypeStepConfiguration
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Configurations.Contracts;

public class ContractTypeStepConfiguration : IEntityTypeConfiguration<ContractTypeStep>
{
    public void Configure(EntityTypeBuilder<ContractTypeStep> builder)
    {
        builder.ToTable("StepContractType");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.StepOrder).IsRequired();

        builder.HasOne(x => x.ContractType)
            .WithMany(ct => ct.Steps)
            .HasForeignKey(x => x.ContractTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // وضعیت شروع مرحله
        builder.HasOne(x => x.StartState)
            .WithMany(s => s.StartSteps)
            .HasForeignKey(x => x.StartStateId)
            .OnDelete(DeleteBehavior.Restrict);

        // وضعیت پایان مرحله
        builder.HasOne(x => x.EndState)
            .WithMany(s => s.EndSteps)
            .HasForeignKey(x => x.EndStateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ContractTypeId);
        builder.HasIndex(x => x.StartStateId);
        builder.HasIndex(x => x.EndStateId);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => new { x.ContractTypeId, x.StepOrder });
    }
}