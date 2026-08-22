// WMS.Persistence/Configurations/Contracts/ContractCategoryConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Configurations.Contracts;

public class ContractCategoryConfiguration : IEntityTypeConfiguration<ContractCategory>
{
    public void Configure(EntityTypeBuilder<ContractCategory> builder)
    {
        builder.ToTable("ContractCategory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

        builder.HasMany(x => x.Contracts)
            .WithMany(c => c.Categories)
            .UsingEntity(j => j.ToTable("ContractCategoryMap"));

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