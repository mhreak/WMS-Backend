using Microsoft.AspNetCore.Mvc;
using WMS.Application.Common.Exceptions;

namespace WMS.API.Helpers;

public static class ErrorSanitizer
{
    public static object? Sanitize(Exception ex, string errorId)
    {
        return new { errorId, message = ex.Message };
    }
}
