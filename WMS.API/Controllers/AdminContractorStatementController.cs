using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractorStatements)]
[Tags("Admin - Contractor Statements")]
public class AdminContractorStatementController : AdminBaseController
{
    private readonly IContractorStatementService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractorStatementController(IContractorStatementService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] ContractorStatementFilterRequest filter, CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatementsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatementsRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractorStatementRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatementCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContractorStatementRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatementUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatementDeleted);
        return Ok(ApiResult.Success(message));
    }

    // مدیریت اقلام اضافه/کسر همین صورت‌وضعیت (جایگزینی کامل لیست)
    // [HttpPut("{id:guid}/extra-or-deductions")]
    // public async Task<IActionResult> SetExtraOrDeductions(Guid id, [FromBody] SetStatementExtraOrDeductionsRequest request, CancellationToken ct)
    // {
    //     var result = await _service.SetExtraOrDeductionsAsync(id, request, ct);
    //     var message = await _localizer.LocalizeAsync(MessageKeys.ContractorStatementExtraOrDeductionsSet);
    //     return Ok(ApiResult.Success(result, message));
    // }
}