// WMS.API/Controllers/AdminNotificationController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Notifications.DTOs;
using WMS.Application.Administrator.Notifications.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.Notifications)]
[Tags("Admin - Notifications")]
public class AdminNotificationController : AdminBaseController
{
    private readonly INotificationService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminNotificationController(
        INotificationService service,
        IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    /// <summary>ایجاد نوتیفیکیشن</summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateNotificationRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.NotificationCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    /// <summary>ویرایش نوتیفیکیشن</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateNotificationRequest request,
        CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.NotificationUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>جزئیات یک نوتیفیکیشن</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.NotificationRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>لیست نوتیفیکیشن‌ها</summary>
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] bool onlyActive = false,
        CancellationToken ct = default)
    {
        var result = await _service.GetListAsync(onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.NotificationsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>حذف نرم نوتیفیکیشن</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.NotificationDeleted);
        return Ok(ApiResult.Success(message));
    }

    /// <summary>
    /// ارزیابی نوتیفیکیشن‌های فعال و برگرداندن موارد مربوط به entityها
    /// </summary>
    [HttpGet("evaluate")]
    public async Task<IActionResult> Evaluate(CancellationToken ct)
    {
        var result = await _service.EvaluateAsync(ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.NotificationsEvaluated);
        return Ok(ApiResult.Success(result, message));
    }
}