using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Statements;

namespace WMS.Persistence.Configurations.Statements;

public class ContractorStatementExtraOrDeductionConfiguration : IEntityTypeConfiguration<ContractorStatementExtraOrDeduction>
{
    public void Configure(EntityTypeBuilder<ContractorStatementExtraOrDeduction> builder)
    {
        builder.ToTable("ContractorStatementExtraOrDeduction");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Amount).IsRequired();

        builder.HasOne(x => x.Rule)
            .WithMany(r => r.StatementItems)
            .HasForeignKey(x => x.RuleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Statement)
            .WithMany(s => s.ExtraOrDeductions)
            .HasForeignKey(x => x.StatementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.RuleId);
        builder.HasIndex(x => x.StatementId);
        builder.HasIndex(x => x.IsDeleted);
    }
}