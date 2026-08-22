// WMS.API/Controllers/Admin/AdminContractorController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Administrator.Contractors.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.Contractors)]
[Tags("Admin - Contractors")]
public class AdminContractorController : AdminBaseController
{
    private readonly IContractorService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractorController(IContractorService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] ContractorFilterRequest filter, CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractorRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContractorRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorDeleted);
        return Ok(ApiResult.Success(message));
    }

    [HttpPatch("{id:guid}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken ct)
    {
        await _service.ToggleActiveAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatusToggled);
        return Ok(ApiResult.Success(message));
    }
}