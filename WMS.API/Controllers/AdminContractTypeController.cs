// WMS.API/Controllers/Admin/AdminContractTypeController.cs
using Microsoft.AspNetCore.Mvc;
using WMS.API.Constants;
using WMS.API.Helpers;
using WMS.Application.Administrator.ContractTypes.DTOs;
using WMS.Application.Administrator.ContractTypes.Interfaces;
using WMS.Application.Common.Localization;

namespace WMS.API.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.ContractTypes)]
[Tags("Admin - Contract Types")]
public class AdminContractTypeController : AdminBaseController
{
    private readonly IContractTypeService _service;
    private readonly IResponseLocalizer _localizer;

    public AdminContractTypeController(IContractTypeService service, IResponseLocalizer localizer)
    {
        _service = service;
        _localizer = localizer;
    }

    /// <summary>لیست با صفحه‌بندی و فیلتر</summary>
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] ContractTypeFilterRequest filter, CancellationToken ct)
    {
        var result = await _service.GetListAsync(filter, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>لیست ساده (برای Dropdown)</summary>
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(onlyActive, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypesRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>جزئیات</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeRetrieved);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>ایجاد</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractTypeRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeCreated);
        return StatusCode(StatusCodes.Status201Created, ApiResult.Success(result, message));
    }

    /// <summary>ویرایش</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContractTypeRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeUpdated);
        return Ok(ApiResult.Success(result, message));
    }

    /// <summary>حذف نرم</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeDeleted);
        return Ok(ApiResult.Success(message));
    }

    /// <summary>تغییر وضعیت فعال/غیرفعال</summary>
    [HttpPatch("{id:guid}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken ct)
    {
        await _service.ToggleActiveAsync(id, ct);
        var message = await _localizer.LocalizeAsync(MessageKeys.ContractTypeStatusToggled);
        return Ok(ApiResult.Success(message));
    }
}