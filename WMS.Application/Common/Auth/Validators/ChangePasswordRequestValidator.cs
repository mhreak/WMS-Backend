using FluentValidation;
using WMS.Application.Common.Auth.DTOs;
using WMS.Application.Common.Localization;

namespace WMS.Application.Common.Auth.Validators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestDto>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage(MessageKeys.PasswordRequired);
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(MessageKeys.PasswordRequired)
            .MinimumLength(6).WithMessage(MessageKeys.PasswordMinLength);
    }
}
