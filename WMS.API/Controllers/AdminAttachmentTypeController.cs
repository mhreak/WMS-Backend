// WMS.API/Controllers/Admin/AdminAttachmentTypeController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Attachments.DTOs;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Enums;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.AttachmentTypes)]
[Tags("Admin - Attachment Types")]
public class AdminAttachmentTypeController : AdminBaseController
{
    private readonly IAttachmentTypeService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminAttachmentTypeController(
        IAttachmentTypeService service,
        IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    /// <summary>لیست انواع پیوست</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] EntityType? entityType = null,
        [FromQuery] bool onlyActive = true,
        CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(entityType, onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentTypesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>جزئیات یک نوع پیوست</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentTypeRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>ایجاد نوع پیوست</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAttachmentTypeRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentTypeCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    /// <summary>ویرایش نوع پیوست</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttachmentTypeRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentTypeUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>حذف نرم نوع پیوست</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentTypeDeleted);
        return Ok(ApiResult.Success(message));
    }

    /// <summary>تغییر وضعیت فعال/غیرفعال</summary>
    [HttpPatch("{id:guid}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken ct)
    {
        await _service.ToggleActiveAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentTypeStatusToggled);
        return Ok(ApiResult.Success(message));
    }
}