using WMS.Application.Common.Auth.Services;
using WMS.Application.Common.Interfaces;
using WMS.Application.Administrator.Contractors.Interfaces;
using WMS.Application.Administrator.Contractors.Services;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Administrator.Labels.Services;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Administrator.Attachments.Services;
using WMS.Application.Administrator.ContractTypes.Interfaces;
using WMS.Application.Administrator.ContractTypes.Services;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Administrator.Contracts.Services;
using WMS.Application.Administrator.Steps.Interfaces;
using WMS.Application.Administrator.ContractTypeSteps.Services;
using WMS.Application.Administrator.ContractTypeStates.Interfaces;
using WMS.Application.Administrator.ContractTypeStates.Services;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Administrator.Statements.Services;
using WMS.Application.Administrator.Alarms.Interfaces;
using WMS.Application.Administrator.Alarms.Services;

namespace WMS.API.DependencyInjection;

public static class CoreApplicationServiceDependencyInjection
{
    public static IServiceCollection AddCoreApplicationServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IRoleContext>(_ => new GenericRoleContext("User"));
        services.AddScoped<IContractorService, ContractorService>();
        services.AddScoped<ILabelService, LabelService>();
        services.AddScoped<IEntityAttachmentService, EntityAttachmentService>();
        services.AddScoped<IContractTypeService, ContractTypeService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IContractStepService, ContractStepService>();
        services.AddScoped<IContractCategoryService, ContractCategoryService>();
        services.AddScoped<IContractStepService, ContractStepService>();
        services.AddScoped<IAttachmentTypeService, AttachmentTypeService>();
        services.AddScoped<IEntityAttachmentService, EntityAttachmentService>();
        services.AddScoped<IStepService, StepService>();
        services.AddScoped<IContractTypeStateService, ContractTypeStateService>();
        services.AddScoped<IExtraOrDeductionTypeService, ExtraOrDeductionTypeService>();
        services.AddScoped<IExtraOrDeductionRuleService, ExtraOrDeductionRuleService>();
        services.AddScoped<IContractorStatementService, ContractorStatementService>();
        services.AddScoped<IDeadlineAlarmService, DeadlineAlarmService>();
        return services;
    }
}
