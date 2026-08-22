// WMS.API/Controllers/Admin/AdminContractStepController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractSteps)]
[Tags("Admin - Contract Steps")]
public class AdminContractStepController : AdminBaseController
{
    private readonly IContractStepService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractStepController(IContractStepService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    /// <summary>لیست مراحل یک قرارداد</summary>
    [HttpGet("by-contract/{contractId:guid}")]
    public async Task<IActionResult> GetByContract(Guid contractId, CancellationToken ct)
    {
        var result = await _service.GetByContractIdAsync(contractId, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractStepsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>افزودن یک مرحله به قرارداد</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractStepRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractStepCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    /// <summary>ویرایش تاریخ‌های یک مرحله</summary>
    [HttpPut("{contractId:guid}/{stepId:guid}")]
    public async Task<IActionResult> Update(
        Guid contractId,
        Guid stepId,
        [FromBody] UpdateContractStepRequest request,
        CancellationToken ct)
    {
        var result = await _service.UpdateAsync(contractId, stepId, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractStepUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>حذف یک مرحله از قرارداد</summary>
    [HttpDelete("{contractId:guid}/{stepId:guid}")]
    public async Task<IActionResult> Delete(Guid contractId, Guid stepId, CancellationToken ct)
    {
        await _service.DeleteAsync(contractId, stepId, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractStepDeleted);
        return Ok(ApiResult.Success(message));
    }

    /// <summary>تنظیم کامل مراحل یک قرارداد (جایگزینی)</summary>
    [HttpPut("set")]
    public async Task<IActionResult> SetSteps([FromBody] SetContractStepsRequest request, CancellationToken ct)
    {
        var result = await _service.SetStepsAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractStepsSet);
        return Ok(ApiResult.Success(result, message));
    }
}