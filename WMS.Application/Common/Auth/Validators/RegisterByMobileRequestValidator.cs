using FluentValidation;
using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Validators;

public class RegisterByMobileRequestValidator : AbstractValidator<RegisterByMobileRequestDto>
{
    public RegisterByMobileRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).IranianMobile();
        RuleFor(x => x.Code).OtpCode();
    }
}
