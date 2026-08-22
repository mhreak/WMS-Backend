using FluentValidation;
using WMS.Application.Administrator.Contractors.DTOs;

namespace WMS.Application.Administrator.Contractors.Validator;

public class CreateContractorCategoryRequestValidator : AbstractValidator<CreateContractorCategoryRequest>
{
    public CreateContractorCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام دسته‌بندی الزامی است.")
            .MaximumLength(150);
    }
}

public class UpdateContractorCategoryRequestValidator : AbstractValidator<UpdateContractorCategoryRequest>
{
    public UpdateContractorCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام دسته‌بندی الزامی است.")
            .MaximumLength(150);
    }
}