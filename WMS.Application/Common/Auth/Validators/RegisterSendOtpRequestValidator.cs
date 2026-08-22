using FluentValidation;
using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Validators;

public class RegisterSendOtpRequestValidator : AbstractValidator<RegisterSendOtpRequestDto>
{
    public RegisterSendOtpRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).IranianMobile();
    }
}
