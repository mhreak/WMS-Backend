using WMS.Domain.Enums;

namespace WMS.Application.Common.Localization;

public interface ICurrentUserLanguageProvider
{
    Task<Language> GetCurrentLanguageAsync();
    void InvalidateCache(Guid userId);
}
