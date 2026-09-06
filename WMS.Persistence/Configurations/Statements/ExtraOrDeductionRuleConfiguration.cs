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

    // Contract حذف شد

    builder.HasOne(x => x.ExtraOrDeductionType)
        .WithMany(t => t.Rules)
        .HasForeignKey(x => x.ExtraOrDeductionTypeId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(x => x.ContractorCategory)
        .WithMany()
        .HasForeignKey(x => x.ContractorCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(x => x.ContractCategory)
        .WithMany()
        .HasForeignKey(x => x.ContractCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(x => x.Contractor)
        .WithMany()
        .HasForeignKey(x => x.ContractorId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(x => x.ContractLabel)
        .WithMany()
        .HasForeignKey(x => x.ContractLabelId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasIndex(x => x.ContractTypeId);
    builder.HasIndex(x => x.ExtraOrDeductionTypeId);
    builder.HasIndex(x => x.ContractorCategoryId);
    builder.HasIndex(x => x.ContractCategoryId);
    builder.HasIndex(x => x.ContractorId);
    builder.HasIndex(x => x.ContractLabelId);
    builder.HasIndex(x => x.IsDeleted);
}
}