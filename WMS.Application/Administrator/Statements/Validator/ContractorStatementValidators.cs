using FluentValidation;
using WMS.Application.Administrator.Statements.DTOs;

namespace WMS.Application.Administrator.Statements.Validator;

public class CreateContractorStatementRequestValidator : AbstractValidator<CreateContractorStatementRequest>
{
    public CreateContractorStatementRequestValidator()
    {
        RuleFor(x => x.ContractId).NotEmpty().WithMessage("قرارداد الزامی است.");
        RuleFor(x => x.ContractTypeStepId).NotEmpty().WithMessage("مرحله الزامی است.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
    }
}

public class UpdateContractorStatementRequestValidator : AbstractValidator<UpdateContractorStatementRequest>
{
    public UpdateContractorStatementRequestValidator()
    {
        RuleFor(x => x.ContractTypeStepId).NotEmpty().WithMessage("مرحله الزامی است.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
    }
}

public class SetStatementExtraOrDeductionsRequestValidator : AbstractValidator<SetStatementExtraOrDeductionsRequest>
{
    public SetStatementExtraOrDeductionsRequestValidator()
    {
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.RuleId).NotEmpty().WithMessage("شناسه قانون الزامی است.");
            item.RuleFor(i => i.Amount).GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
        });
    }
}