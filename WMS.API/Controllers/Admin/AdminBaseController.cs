using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.API.Filters;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
public abstract class AdminBaseController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException(MessageKeys.InvalidCredentials);
        return userId;
    }
}
