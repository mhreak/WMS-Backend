// WMS.API/Controllers/Admin/AdminContractTypeStateController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.ContractTypeStates.DTOs;
using WMS.Application.Administrator.ContractTypeStates.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractTypeStates)]
[Tags("Admin - Contract Type States")]
public class AdminContractTypeStateController : AdminBaseController
{
    private readonly IContractTypeStateService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractTypeStateController(
        IContractTypeStateService service,
        IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] ContractTypeStateFilterRequest filter,
        CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStatesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool onlyActive = true,
        CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStatesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStateRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateContractTypeStateRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStateCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateContractTypeStateRequest request,
        CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStateUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStateDeleted);
        return Ok(ApiResult.Success(message));
    }

    [HttpPatch("{id:guid}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken ct)
    {
        await _service.ToggleActiveAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStateStatusToggled);
        return Ok(ApiResult.Success(message));
    }
}