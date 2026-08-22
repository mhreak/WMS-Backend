// WMS.API/Controllers/Admin/AdminLabelController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Labels.DTOs;
using WMS.Application.Administrator.Labels.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Enums;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.Labels)]
[Tags("Admin - Labels")]
public class AdminLabelController : AdminBaseController
{
    private readonly ILabelService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminLabelController(ILabelService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }


    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] LabelFilterRequest filter, CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelRetrieved);
        return Ok(ApiResult.Success(result, message));
    }


    [HttpGet("by-entity-type/{entityType}")]
    public async Task<IActionResult> GetByEntityType(
        EntityType entityType,
        [FromQuery] bool onlyActive = true,
        CancellationToken ct = default)
    {
        var result = await _service.GetByEntityTypeAsync(entityType, onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLabelRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLabelRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelDeleted);
        return Ok(ApiResult.Success(message));
    }

   
    [HttpPatch("{id:guid}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken ct)
    {
        await _service.ToggleActiveAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.LabelStatusToggled);
        return Ok(ApiResult.Success(message));
    }
}