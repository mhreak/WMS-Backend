// WMS.Persistence/Configurations/Contracts/ContractTypeStateConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Configurations.Contracts;

public class ContractTypeStateConfiguration : IEntityTypeConfiguration<ContractTypeState>
{
    public void Configure(EntityTypeBuilder<ContractTypeState> builder)
    {
        builder.ToTable("ContractTypeState");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
    }
}