using FluentValidation;
using WMS.Application.Common.File.DTOs;
using WMS.Domain.Enums;

namespace WMS.Application.Common.File.Validators;

public class FileAttachmentValidator : AbstractValidator<GenericUploadFileRequest>
{
    public FileAttachmentValidator()
    {
        RuleFor(x => x.File).NotNull();
        RuleFor(x => x.File.Length).GreaterThan(0);
    }
}
