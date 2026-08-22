using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Contractors;

namespace WMS.Persistence.Configurations.Contractors;

public class ContractorConfiguration : IEntityTypeConfiguration<Contractor>
{
    public void Configure(EntityTypeBuilder<Contractor> builder)
    {
        builder.ToTable("Contractor");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.CompanyName).HasMaxLength(200);

        builder.Property(x => x.Mobile1).HasMaxLength(20);
        builder.Property(x => x.Mobile2).HasMaxLength(20);
        builder.Property(x => x.Phone1).HasMaxLength(20);
        builder.Property(x => x.Phone2).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(150);

        builder.Property(x => x.NationalCode).HasMaxLength(20);
        builder.Property(x => x.EconomicCode).HasMaxLength(20);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Categories)
            .WithMany(c => c.Contractors)
            .UsingEntity(j => j.ToTable("ContractorCategoryMap"));


        builder.HasIndex(x => x.NationalCode);
        builder.HasIndex(x => x.EconomicCode);
        builder.HasIndex(x => x.Mobile1);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.Type);
    }
}