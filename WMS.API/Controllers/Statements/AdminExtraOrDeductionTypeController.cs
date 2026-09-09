using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ExtraOrDeductionTypes)]
[Tags("Admin - Extra/Deduction Types")]
public class AdminExtraOrDeductionTypeController : AdminBaseController
{
    private readonly IExtraOrDeductionTypeService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminExtraOrDeductionTypeController(IExtraOrDeductionTypeService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ExtraOrDeductionTypesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExtraOrDeductionTypeRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ExtraOrDeductionTypeCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExtraOrDeductionTypeRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ExtraOrDeductionTypeUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ExtraOrDeductionTypeDeleted);
        return Ok(ApiResult.Success(message));
    }
}