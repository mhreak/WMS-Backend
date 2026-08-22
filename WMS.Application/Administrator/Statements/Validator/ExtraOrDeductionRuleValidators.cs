using FluentValidation;
using WMS.Application.Administrator.Statements.DTOs;

namespace WMS.Application.Administrator.Statements.Validator;

public class CreateExtraOrDeductionRuleRequestValidator : AbstractValidator<CreateExtraOrDeductionRuleRequest>
{
    public CreateExtraOrDeductionRuleRequestValidator()
    {
        RuleFor(x => x.ContractTypeId).NotEmpty().WithMessage("نوع قرارداد الزامی است.");
        RuleFor(x => x.ExtraOrDeductionTypeId).NotEmpty().WithMessage("نوع اضافه/کسر الزامی است.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
        RuleFor(x => x.Amount)
            .LessThanOrEqualTo(100)
            .When(x => x.AmountType == Domain.Enums.AmountType.Percentage)
            .WithMessage("درصد نمی‌تواند بیشتر از ۱۰۰ باشد.");
    }
}

public class UpdateExtraOrDeductionRuleRequestValidator : AbstractValidator<UpdateExtraOrDeductionRuleRequest>
{
    public UpdateExtraOrDeductionRuleRequestValidator()
    {
        RuleFor(x => x.ContractTypeId).NotEmpty().WithMessage("نوع قرارداد الزامی است.");
        RuleFor(x => x.ExtraOrDeductionTypeId).NotEmpty().WithMessage("نوع اضافه/کسر الزامی است.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("مبلغ باید بزرگ‌تر از صفر باشد.");
        RuleFor(x => x.Amount)
            .LessThanOrEqualTo(100)
            .When(x => x.AmountType == Domain.Enums.AmountType.Percentage)
            .WithMessage("درصد نمی‌تواند بیشتر از ۱۰۰ باشد.");
    }
}