using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Statements;

namespace WMS.Persistence.Configurations.Statements;

public class ExtraOrDeductionRuleConfiguration : IEntityTypeConfiguration<ExtraOrDeductionRule>
{
    public void Configure(EntityTypeBuilder<ExtraOrDeductionRule> builder)
    {
        builder.ToTable("ExtraOrDeductionRule");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.AmountType).IsRequired().HasConversion<int>();
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.ContractorType).HasConversion<int>();

        builder.HasOne(x => x.ContractType)
            .WithMany()
            .HasForeignKey(x => x.ContractTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExtraOrDeductionType)
            .WithMany(t => t.Rules)
            .HasForeignKey(x => x.ExtraOrDeductionTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Contractor)
            .WithMany()
            .HasForeignKey(x => x.ContractorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Label)
            .WithMany()
            .HasForeignKey(x => x.LabelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ContractTypeId);
        builder.HasIndex(x => x.ContractId);
        builder.HasIndex(x => x.ExtraOrDeductionTypeId);
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.ContractorId);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.LabelId);
    }
}