namespace WMS.Application.Common.Localization;

public interface ILocalizationService
{
    string Get(string key, WMS.Domain.Enums.Language language, params object[] args);
}
