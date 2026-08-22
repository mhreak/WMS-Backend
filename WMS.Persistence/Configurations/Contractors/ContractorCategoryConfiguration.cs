using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contractors;

namespace WMS.Persistence.Configurations.Contractors;

public class ContractorCategoryConfiguration : IEntityTypeConfiguration<ContractorCategory>
{
    public void Configure(EntityTypeBuilder<ContractorCategory> builder)
    {
        builder.ToTable("ContractorCategory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.ParentId);
    }
}