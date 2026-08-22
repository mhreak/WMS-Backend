using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Entities.Locations;
using WMS.Domain.Entities.Users;
using WMS.Domain.Entities.Contractors;
using WMS.Domain.Entities.Labels;
using WMS.Domain.Entities.Attachments;
using WMS.Domain.Entities.ContractTypes;
using WMS.Domain.Entities.Contracts;
using WMS.Domain.Entities.Statements;


namespace WMS.Persistence.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<City> Cities => Set<City>();
    // public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<FileAsset> Files => Set<FileAsset>();
    public DbSet<Contractor> Contractors => Set<Contractor>();
    public DbSet<ContractorCategory> ContractorCategories => Set<ContractorCategory>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<EntityLabel> EntityLabels => Set<EntityLabel>();
    public DbSet<AttachmentType> AttachmentTypes => Set<AttachmentType>();
    public DbSet<EntityAttachment> EntityAttachments => Set<EntityAttachment>();
    public DbSet<ContractType> ContractTypes => Set<ContractType>();
    public DbSet<ContractTypeStep> ContractTypeStep => Set<ContractTypeStep>();
    public DbSet<Contract> Contract => Set<Contract>();
    public DbSet<ContractStep> Contract_ContractTypeStep => Set<ContractStep>();
    public DbSet<ContractCategory> ContractCategory => Set<ContractCategory>();
    public DbSet<ContractTypeState> ContractTypeStates => Set<ContractTypeState>();
    public DbSet<ContractorStatement> ContractorStatement => Set<ContractorStatement>();
    public DbSet<ExtraOrDeductionType> ExtraOrDeductionType => Set<ExtraOrDeductionType>();
    public DbSet<ExtraOrDeductionRule> ExtraOrDeductionRule => Set<ExtraOrDeductionRule>();
    public DbSet<ContractorStatementExtraOrDeduction> ContractorStatementExtraOrDeduction => Set<ContractorStatementExtraOrDeduction>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
