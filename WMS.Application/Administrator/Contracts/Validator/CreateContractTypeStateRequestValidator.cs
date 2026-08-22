using FluentValidation;
using WMS.Application.Administrator.ContractTypeStates.DTOs;

namespace WMS.Application.Administrator.ContractTypeStates.Validators;

public class CreateContractTypeStateRequestValidator : AbstractValidator<CreateContractTypeStateRequest>
{
    public CreateContractTypeStateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان وضعیت الزامی است.")
            .MaximumLength(150);
    }
}

public class UpdateContractTypeStateRequestValidator : AbstractValidator<UpdateContractTypeStateRequest>
{
    public UpdateContractTypeStateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان وضعیت الزامی است.")
            .MaximumLength(150);
    }
}