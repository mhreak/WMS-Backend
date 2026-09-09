// WMS.API/Controllers/Admin/AdminContractController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.ContractBoard.DTOs;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.Contracts)]
[Tags("Admin - Contracts")]
public class AdminContractController : AdminBaseController
{
    private readonly IContractService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractController(IContractService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] ContractFilterRequest filter, CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContractRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractDeleted);
        return Ok(ApiResult.Success(message));
    }
    [HttpGet("board")]
    public async Task<IActionResult> GetBoard(
        [FromQuery] ContractBoardFilterRequest filter,
        CancellationToken ct)
    {
        var result = await _service.GetBoardAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>
    /// جابجایی قرارداد بین مراحل (Drag & Drop)
    /// </summary>
    [HttpPut("{contractId:guid}/move-step")]
    public async Task<IActionResult> MoveStep(
        Guid contractId,
        [FromBody] MoveContractStepRequest request,
        CancellationToken ct)
    {
        await _service.MoveStepAsync(contractId, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractStepUpdated);
        return Ok(ApiResult.Success(message));
    }

}