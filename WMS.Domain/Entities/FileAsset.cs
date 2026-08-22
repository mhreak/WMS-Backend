using System.ComponentModel.DataAnnotations;
using WMS.Domain.Common;

namespace WMS.Domain.Entities;

public class FileAsset : BaseEntity
{
    [Required]
    [MaxLength(300)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Extension { get; set; } = string.Empty;

    public long Size { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Path { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string UploadFileType { get; set; } = string.Empty;

    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public Guid UploaderId { get; set; }

    [Required]
    public WMS.Domain.Enums.UploadFileType FileType { get; set; }
}
