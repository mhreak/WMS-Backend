using FluentValidation;
using WMS.Application.Administrator.Statements.DTOs;

namespace WMS.Application.Administrator.Statements.Validator;

public class CreateExtraOrDeductionTypeRequestValidator : AbstractValidator<CreateExtraOrDeductionTypeRequest>
{
    public CreateExtraOrDeductionTypeRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان الزامی است.").MaximumLength(200);
    }
}

public class UpdateExtraOrDeductionTypeRequestValidator : AbstractValidator<UpdateExtraOrDeductionTypeRequest>
{
    public UpdateExtraOrDeductionTypeRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان الزامی است.").MaximumLength(200);
    }
}