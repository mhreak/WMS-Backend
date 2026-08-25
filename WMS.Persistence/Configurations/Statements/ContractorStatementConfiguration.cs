using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Statements;

namespace WMS.Persistence.Configurations.Statements;

public class ContractorStatementConfiguration : IEntityTypeConfiguration<ContractorStatement>
{
    public void Configure(EntityTypeBuilder<ContractorStatement> builder)
    {
        builder.ToTable("ContractorStatement");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.StatementDate).IsRequired();
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ContractTypeStep)
            .WithMany()
            .HasForeignKey(x => x.ContractTypeStepId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Attachment)
            .WithMany()
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ContractId);
        builder.HasIndex(x => x.ContractTypeStepId);
        builder.HasIndex(x => x.FileId);
        builder.HasIndex(x => x.IsDeleted);
    }
}