// WMS.Persistence/Configurations/Contracts/ContractConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contracts;

namespace WMS.Persistence.Configurations.Contracts;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contract");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
        builder.Property(x => x.ContractNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ContractAmount).IsRequired();

        builder.HasOne(x => x.Contractor)
            .WithMany()
            .HasForeignKey(x => x.ContractorId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        
        builder.HasOne(x => x.ContractTypeState)
        .WithMany()
        .HasForeignKey(x => x.ContractTypeStateId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ContractNumber).IsUnique();
        builder.HasIndex(x => x.ContractorId);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.ContractTypeStateId);
        

        // داخل ContractConfiguration:



    }
}