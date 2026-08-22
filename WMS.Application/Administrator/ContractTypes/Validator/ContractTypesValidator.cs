using FluentValidation;
using WMS.Application.Administrator.ContractTypes.DTOs;

public class CreateContractTypeRequestValidator : AbstractValidator<CreateContractTypeRequest>
{
    public CreateContractTypeRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نوع قرارداد الزامی است.")
            .MaximumLength(200);
    }
}

public class UpdateContractTypeRequestValidator : AbstractValidator<UpdateContractTypeRequest>
{
    public UpdateContractTypeRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان نوع قرارداد الزامی است.")
            .MaximumLength(200);
    }
}