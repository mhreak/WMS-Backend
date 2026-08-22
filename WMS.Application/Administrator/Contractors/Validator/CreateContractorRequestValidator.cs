// CreateContractorRequestValidator.cs
using FluentValidation;
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Domain.Enums;

public class CreateContractorRequestValidator : AbstractValidator<CreateContractorRequest>
{
    public CreateContractorRequestValidator()
    {
        RuleFor(x => x.Type).IsInEnum().WithMessage("نوع پیمانکار معتبر نیست.");

        When(x => x.Type == ContractorType.Individual, () =>
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("نام الزامی است.").MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().WithMessage("نام خانوادگی الزامی است.").MaximumLength(100);
        });

        When(x => x.Type == ContractorType.Legal, () =>
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("نام شرکت الزامی است.").MaximumLength(200);
        });

        RuleFor(x => x.Mobile1)
            .MaximumLength(20)
            .Matches(@"^(\+98|0)?9\d{9}$")
            .When(x => !string.IsNullOrWhiteSpace(x.Mobile1))
            .WithMessage("فرمت موبایل معتبر نیست.");

        RuleFor(x => x.Mobile2)
            .MaximumLength(20)
            .Matches(@"^(\+98|0)?9\d{9}$")
            .When(x => !string.IsNullOrWhiteSpace(x.Mobile2))
            .WithMessage("فرمت موبایل دوم معتبر نیست.");

        RuleFor(x => x.Phone1).MaximumLength(20);
        RuleFor(x => x.Phone2).MaximumLength(20);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("فرمت ایمیل معتبر نیست.")
            .MaximumLength(150);

        RuleFor(x => x.NationalCode)
            .Matches(@"^\d{10}$")
            .When(x => !string.IsNullOrWhiteSpace(x.NationalCode))
            .WithMessage("کد ملی باید ۱۰ رقم باشد.");

        RuleFor(x => x.EconomicCode).MaximumLength(20);
        RuleFor(x => x.CityId).NotEmpty().When(x => x.CityId.HasValue);
    }
}

// public class UpdateContractorRequestValidator : AbstractValidator<UpdateContractorRequest>
// {
//     public UpdateContractorRequestValidator()
//     {
//         Include(new CreateContractorRequestValidator()); 
       
//     }
// }