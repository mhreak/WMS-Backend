namespace WMS.Application.Common.Localization;

public interface IResponseLocalizer
{
    Task<string> LocalizeAsync(string key, params object[] args);
}
