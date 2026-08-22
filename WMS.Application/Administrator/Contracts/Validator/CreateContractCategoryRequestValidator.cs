using FluentValidation;
using WMS.Application.Administrator.ContractCategories.DTOs;

public class CreateContractCategoryRequestValidator : AbstractValidator<CreateContractCategoryRequest>
{
    public CreateContractCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام دسته‌بندی الزامی است.")
            .MaximumLength(150);

        
    }
}

public class UpdateContractCategoryRequestValidator : AbstractValidator<UpdateContractCategoryRequest>
{
    public UpdateContractCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام دسته‌بندی الزامی است.")
            .MaximumLength(150);

    }
}