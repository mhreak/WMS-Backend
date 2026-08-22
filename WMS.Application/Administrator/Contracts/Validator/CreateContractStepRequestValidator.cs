using FluentValidation;
using WMS.Application.Administrator.Contracts.DTOs;

public class CreateContractStepRequestValidator : AbstractValidator<CreateContractStepRequest>
{
    public CreateContractStepRequestValidator()
    {
        RuleFor(x => x.ContractId).NotEmpty().WithMessage("شناسه قرارداد الزامی است.");
        RuleFor(x => x.StepId).NotEmpty().WithMessage("شناسه مرحله الزامی است.");

        RuleFor(x => x)
            .Must(x => x.FinishDate == null || x.FinishDate >= x.StartDate)
            .WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");
    }
}

public class UpdateContractStepRequestValidator : AbstractValidator<UpdateContractStepRequest>
{
    public UpdateContractStepRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => x.FinishDate >= x.StartDate)
            .WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");
    }
}

public class SetContractStepsRequestValidator : AbstractValidator<SetContractStepsRequest>
{
    public SetContractStepsRequestValidator()
    {
        RuleFor(x => x.ContractId).NotEmpty().WithMessage("شناسه قرارداد الزامی است.");

        RuleFor(x => x.Steps)
            .NotNull().WithMessage("لیست مراحل الزامی است.");

        RuleForEach(x => x.Steps).ChildRules(step =>
        {
            step.RuleFor(s => s.StepId).NotEmpty().WithMessage("شناسه مرحله الزامی است.");
            step.RuleFor(s => s)
                .Must(s => s.FinishDate >= s.StartDate)
                .WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");
        });
    }
}