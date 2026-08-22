using FluentValidation;
using WMS.Application.Common.Auth.DTOs;
using WMS.Application.Common.Auth.Validators;

namespace WMS.Application.Common.Auth.Validators;

public class SendOtpForAuthRequestValidator : AbstractValidator<SendOtpForAuthRequestDto>
{
    public SendOtpForAuthRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).IranianMobile();
    }
}
