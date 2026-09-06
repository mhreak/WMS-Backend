using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;

namespace WMS.Application.Common.File.Services;

public interface IImageThumbnailService
{
    bool IsImage(string extension);
    bool IsConvertibleToWebp(string extension);
    Task<string> SaveAsWebpAsync(Stream sourceStream, string destinationPathWithoutExtension, CancellationToken ct = default);
    Task GenerateThumbnailAsync(string sourcePath, string destinationPath, int targetWidth, CancellationToken ct = default);
}

public class ImageThumbnailService : IImageThumbnailService
{
    private static readonly HashSet<string> ImageExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp"
    };

    // gif رو عمداً کنار گذاشتیم چون تبدیلش به webp انیمیشن رو از بین می‌بره
    private static readonly HashSet<string> ConvertibleExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".bmp"
    };

    public bool IsImage(string extension) => ImageExtensions.Contains(extension);
    public bool IsConvertibleToWebp(string extension) => ConvertibleExtensions.Contains(extension);

    public async Task<string> SaveAsWebpAsync(Stream sourceStream, string destinationPathWithoutExtension, CancellationToken ct = default)
    {
        using var image = await Image.LoadAsync(sourceStream, ct);

        var destinationPath = destinationPathWithoutExtension + ".webp";
        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        await image.SaveAsync(destinationPath, new WebpEncoder
        {
            Quality = 85,
            FileFormat = WebpFileFormatType.Lossy
        }, ct);

        return destinationPath;
    }

    public async Task GenerateThumbnailAsync(string sourcePath, string destinationPath, int targetWidth, CancellationToken ct = default)
    {
        var targetHeight = targetWidth / 2; // نسبت 2:1

        using var image = await Image.LoadAsync(sourcePath, ct);

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Crop,
            Size = new Size(targetWidth, targetHeight),
            Position = AnchorPositionMode.Center
        }));

        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        await image.SaveAsync(destinationPath, new WebpEncoder
        {
            Quality = 80,
            FileFormat = WebpFileFormatType.Lossy
        }, ct);   // تامبنیل هم دیگه webp ذخیره می‌شه، نه jpg
    }
}