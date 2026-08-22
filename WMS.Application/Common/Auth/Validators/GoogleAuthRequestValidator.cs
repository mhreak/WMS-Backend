using FluentValidation;
using WMS.Application.Common.Auth.DTOs;

namespace WMS.Application.Common.Auth.Validators;

public class GoogleAuthRequestValidator : AbstractValidator<GoogleAuthRequestDto>
{
    public GoogleAuthRequestValidator()
    {
        RuleFor(x => x.IdToken).NotEmpty();
    }
}
