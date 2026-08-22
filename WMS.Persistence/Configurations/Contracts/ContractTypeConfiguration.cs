// WMS.Persistence/Configurations/Contracts/ContractTypeConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.ContractTypes;

namespace WMS.Persistence.Configurations.Contracts;

public class ContractTypeConfiguration : IEntityTypeConfiguration<ContractType>
{
    public void Configure(EntityTypeBuilder<ContractType> builder)
    {
        builder.ToTable("ContractType");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
    }
}