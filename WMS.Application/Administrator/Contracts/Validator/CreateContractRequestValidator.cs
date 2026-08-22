using FluentValidation;
using WMS.Application.Administrator.Contracts.DTOs;

public class CreateContractRequestValidator : AbstractValidator<CreateContractRequest>
{
    public CreateContractRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان قرارداد الزامی است.")
            .MaximumLength(300);

        RuleFor(x => x.ContractNumber)
            .NotEmpty().WithMessage("شماره قرارداد الزامی است.")
            .MaximumLength(20);

        RuleFor(x => x.ContractAmount)
        .GreaterThanOrEqualTo(0)
        .WithMessage("مبلغ قرارداد نمی‌تواند منفی باشد.");

        RuleFor(x => x)
            .Must(x => x.FinishedDate >= x.StartDate || !x.FinishedDate.HasValue || x.FinishedDate >= x.StartDate)
            .WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");

        RuleForEach(x => x.CategoryIds)
            .NotEmpty()
            .When(x => x.CategoryIds != null)
            .WithMessage("شناسه دسته‌بندی نامعتبر است.");
    }
}

public class UpdateContractRequestValidator : AbstractValidator<UpdateContractRequest>
{
    public UpdateContractRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان قرارداد الزامی است.")
            .MaximumLength(300);

        RuleFor(x => x.ContractNumber)
            .NotEmpty().WithMessage("شماره قرارداد الزامی است.")
            .MaximumLength(20);

        RuleFor(x => x.ContractAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("مبلغ قرارداد نمی‌تواند منفی باشد.");

        RuleFor(x => x)
            .Must(x => x.FinishedDate >= x.StartDate || !x.FinishedDate.HasValue || x.FinishedDate >= x.StartDate)
            .WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");
    }
}