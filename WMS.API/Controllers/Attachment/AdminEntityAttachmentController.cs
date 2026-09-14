// WMS.API/Controllers/Admin/AdminEntityAttachmentController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Attachments.DTOs;
using WMS.Application.Administrator.Attachments.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.EntityAttachments)]
[Tags("Admin - Entity Attachments")]
public class AdminEntityAttachmentController : AdminBaseController
{
    private readonly IEntityAttachmentService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminEntityAttachmentController(
        IEntityAttachmentService service,
        IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    /// <summary>آپلود فایل و اتصال به موجودیت</summary>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadEntityAttachmentRequest request, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var result = await _service.UploadAsync(request, userId, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentAttached);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    /// <summary>لیست پیوست‌های یک موجودیت</summary>
    [HttpGet("by-entity/{entityId:guid}")]
    public async Task<IActionResult> GetByEntity(
        Guid entityId,
        [FromQuery] Guid? attachmentTypeId = null,
        CancellationToken ct = default)
    {
        var result = await _service.GetByEntityIdAsync(entityId, attachmentTypeId, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>جزئیات یک پیوست</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>حذف پیوست</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.AttachmentDetached);
        return Ok(ApiResult.Success(message));
    }
}