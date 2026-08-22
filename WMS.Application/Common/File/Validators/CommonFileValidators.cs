using FluentValidation;
using WMS.Application.Common.File.DTOs;
using WMS.Domain.Enums;

namespace WMS.Application.Common.File.Validators;

public interface IFileAttachmentValidator : IValidator<GenericUploadFileRequest> { }

public class CommonFileValidators
{
    public static bool IsValidExtension(string fileName, IEnumerable<string> allowedExtensions)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return allowedExtensions.Contains(ext);
    }
}
