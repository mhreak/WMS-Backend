// WMS.API/Controllers/Admin/AdminContractorCategoryController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Alarms.DTOs;
using WMS.Application.Administrator.Alarms.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.DeadlineAlarm)]
[Tags("Admin - DeadlineAlarm")]
public class AdminDeadlineAlarmController : AdminBaseController
{
    private readonly IDeadlineAlarmService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminDeadlineAlarmController(IDeadlineAlarmService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeadlineAlarmRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.DeadlineAlarmCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.DeadlineAlarmRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("due")]
    public async Task<IActionResult> GetDue(CancellationToken ct)
    {
        var result = await _service.GetDueAlarmsAsync(ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.DueAlarmsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }
}