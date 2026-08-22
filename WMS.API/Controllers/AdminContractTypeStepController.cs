// WMS.API/Controllers/Admin/AdminStepController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.ContractTypeSteps.DTOs;
using WMS.Application.Administrator.Steps.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractTypeSteps)]
[Tags("Admin - Contract Type Steps")]
public class AdminStepController : AdminBaseController
{
    private readonly IStepService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminStepController(IStepService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] StepFilterRequest filter, CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("by-contract-type/{contractTypeId:guid}")]
    public async Task<IActionResult> GetByContractType(
        Guid contractTypeId,
        [FromQuery] bool onlyActive = true,
        CancellationToken ct = default)
    {
        var result = await _service.GetByContractTypeAsync(contractTypeId, onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStepRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStepRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepDeleted);
        return Ok(ApiResult.Success(message));
    }

    [HttpPatch("{id:guid}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken ct)
    {
        await _service.ToggleActiveAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.StepStatusToggled);
        return Ok(ApiResult.Success(message));
    }
}