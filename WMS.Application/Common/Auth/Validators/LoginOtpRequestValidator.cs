using FluentValidation;
using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Validators;

public class LoginOtpRequestValidator : AbstractValidator<LoginOtpRequestDto>
{
    public LoginOtpRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).IranianMobile();
        RuleFor(x => x.Code).OtpCode();
    }
}
