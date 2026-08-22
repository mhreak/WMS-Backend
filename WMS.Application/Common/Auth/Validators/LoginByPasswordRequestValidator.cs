using FluentValidation;
using WMS.Application.Common.Auth.DTOs;
using WMS.Application.Common.Localization;

namespace WMS.Application.Common.Auth.Validators;

public class LoginByPasswordRequestValidator : AbstractValidator<LoginByPasswordRequestDto>
{
    public LoginByPasswordRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(MessageKeys.UsernameRequired)
            .MaximumLength(50);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(MessageKeys.PasswordRequired);
    }
}
