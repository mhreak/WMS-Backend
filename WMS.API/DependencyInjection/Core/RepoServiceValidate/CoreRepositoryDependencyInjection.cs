using WMS.Application.Administrator.Auth.Interfaces;
using WMS.Application.Administrator.Auth.Services;
using WMS.Application.Common.Auth;
using WMS.Application.Common.Auth.Interfaces;
using WMS.Application.Common.Auth.Services;
using WMS.Application.Common.File.Interfaces;
using WMS.Application.Common.File.Services;
using WMS.Application.Common.Interfaces;
using WMS.Infrastructure.Authentication;
using WMS.Persistence.Repositories;
using WMS.Persistence.Repositories.File;
using WMS.Persistence.UnitOfWork;
using WMS.Application.Common.Location.Interfaces;
using WMS.Persistence.Services;
using WMS.Application.Common.Profile.Interfaces;
using WMS.Application.Common.Profile.Services;
using WMS.Application.Administrator.Contractors.Interfaces;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Administrator.ContractTypes.Interfaces;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Persistence.Repositories.Statements;



using WMS.Persistence.Repositories.Contractors;
using WMS.Persistence.Repositories.Labels;
using WMS.Persistence.Repositories.Attachments;
using WMS.Persistence.Repositories.ContractTypes;
using WMS.Persistence.Repositories.Contracts;
using WMS.Application.Administrator.ContractTypeSteps.Interfaces;
using WMS.Persistence.Repositories.ContractTypeSteps;
using WMS.Application.Administrator.ContractTypeStates.Interfaces;
using WMS.Persistence.Repositories.ContractTypeStates;

using WMS.Application.Administrator.Notifications.Interfaces;
using WMS.Application.Administrator.Notifications.Services;
using WMS.Persistence.Repositories.Notifications;


namespace WMS.API.DependencyInjection;

public static class CoreRepositoryDependencyInjection
{
    public static IServiceCollection AddCoreRepositoryDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IFileAssetRepository, FileAssetRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<RoleAuthCore>();
        services.AddScoped<IRegisterSendOtpService, RegisterSendOtpService>();
        services.AddScoped<IRefreshAccessTokenService, RefreshAccessTokenService>();
        services.AddScoped<IAdminRegisterService, AdminRegisterService>();
        services.AddScoped<IAdminLoginService, AdminLoginService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IContractorRepository, ContractorRepository>();
        services.AddScoped<ILabelRepository, LabelRepository>();
        services.AddScoped<IEntityAttachmentRepository, EntityAttachmentRepository>();
        services.AddScoped<IContractTypeRepository, ContractTypeRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IContractStepRepository, ContractStepRepository>();
        services.AddScoped<IContractCategoryRepository, ContractCategoryRepository>();
        services.AddScoped<IContractStepRepository, ContractStepRepository>();
        services.AddScoped<IAttachmentTypeRepository, AttachmentTypeRepository>();
        services.AddScoped<IEntityAttachmentRepository, EntityAttachmentRepository>();
        services.AddScoped<IStepRepository, StepRepository>();
        services.AddScoped<IContractTypeStateRepository, ContractTypeStateRepository>();
        services.AddScoped<IExtraOrDeductionTypeRepository, ExtraOrDeductionTypeRepository>();
        services.AddScoped<IExtraOrDeductionRuleRepository, ExtraOrDeductionRuleRepository>();
        services.AddScoped<IContractorStatementRepository, ContractorStatementRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}
