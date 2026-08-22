// WMS.API/Controllers/Admin/AdminContractCategoryController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.ContractCategories.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractCategories)]
[Tags("Admin - Contract Categories")]
public class AdminContractCategoryController : AdminBaseController
{
    private readonly IContractCategoryService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractCategoryController(
        IContractCategoryService service,
        IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    /// <summary>
    /// لیست دسته‌بندی‌های قرارداد (همراه ParentName)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool onlyActive = true,
        CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractCategoriesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>
    /// جزئیات یک دسته‌بندی
    /// </summary>
    // [HttpGet("{id:guid}")]
    // public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    // {
    //     var result = await _service.GetByIdAsync(id, ct);
    //     var message = await _localizer.LocalizeAsync(MessageKeys.ContractCategoryRetrieved);
    //     return Ok(ApiResult.Success(result, message));
    // }

    /// <summary>
    /// ایجاد دسته‌بندی (ParentId اختیاری)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateContractCategoryRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractCategoryCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    /// <summary>
    /// ویرایش دسته‌بندی (ParentId اختیاری)
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateContractCategoryRequest request,
        CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractCategoryUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>
    /// حذف نرم دسته‌بندی
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractCategoryDeleted);
        return Ok(ApiResult.Success(message));
    }
}