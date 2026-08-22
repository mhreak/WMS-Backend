using FluentValidation;
using WMS.Application.Administrator.Labels.DTOs;

public class CreateLabelRequestValidator : AbstractValidator<CreateLabelRequest>
{
    public CreateLabelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام برچسب الزامی است.")
            .MaximumLength(100);

        RuleFor(x => x.Color).MaximumLength(20);
        RuleFor(x => x.EntityType).IsInEnum().WithMessage("نوع موجودیت معتبر نیست.");
    }
}

public class UpdateLabelRequestValidator : AbstractValidator<UpdateLabelRequest>
{
    public UpdateLabelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام برچسب الزامی است.")
            .MaximumLength(100);

        RuleFor(x => x.Color).MaximumLength(20);
        RuleFor(x => x.EntityType).IsInEnum().WithMessage("نوع موجودیت معتبر نیست.");
    }
}