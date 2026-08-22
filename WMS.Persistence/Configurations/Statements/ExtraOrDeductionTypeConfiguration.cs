using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Statements;

namespace WMS.Persistence.Configurations.Statements;

public class ExtraOrDeductionTypeConfiguration : IEntityTypeConfiguration<ExtraOrDeductionType>
{
    public void Configure(EntityTypeBuilder<ExtraOrDeductionType> builder)
    {
        builder.ToTable("ExtraOrDeductionType");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
    }
}