// WMS.API/Controllers/Admin/AdminContractorCategoryController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.Contractors.DTOs;
using WMS.Application.Administrator.Contractors.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractorCategories)]
[Tags("Admin - Contractor Categories")]
public class AdminContractorCategoryController : AdminBaseController
{
    private readonly IContractorService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractorCategoryController(
        IContractorService service,
        IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var result = await _service.GetCategoriesAsync(onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorCategoriesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractorCategoryRequest request, CancellationToken ct)
    {
        var result = await _service.CreateCategoryAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorCategoryCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContractorCategoryRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateCategoryAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorCategoryUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteCategoryAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractorCategoryDeleted);
        return Ok(ApiResult.Success(message));
    }
}