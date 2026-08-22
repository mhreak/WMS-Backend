// WMS.Persistence/Configurations/Contracts/ContractStepConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Configurations.Contracts;

public class ContractStepConfiguration : IEntityTypeConfiguration<ContractStep>
{
    public void Configure(EntityTypeBuilder<ContractStep> builder)
    {
        builder.ToTable("Contract_ContractTypeStep");

        // Composite Key
        builder.HasKey(x => new { x.ContractId, x.StepId });

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.FinishDate).IsRequired(false);
        builder.Property(x => x.DeadlineDate);

        builder.HasOne(x => x.Contract)
            .WithMany(c => c.ContractSteps)
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(x => x.ContractId);
        builder.HasIndex(x => x.StepId);
        builder.HasIndex(x => x.DeadlineDate);
    }
}