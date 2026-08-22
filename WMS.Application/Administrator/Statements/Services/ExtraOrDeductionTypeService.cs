using WMS.Application.Administrator.Statements.DTOs;
using WMS.Application.Administrator.Statements.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Statements;

namespace WMS.Application.Administrator.Statements.Services;

public class ExtraOrDeductionTypeService : IExtraOrDeductionTypeService
{
    private readonly IExtraOrDeductionTypeRepository _repo;
    private readonly IUnitOfWork _uow;

    public ExtraOrDeductionTypeService(IExtraOrDeductionTypeRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<List<ExtraOrDeductionTypeDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
    {
        var list = await _repo.GetAllAsync(onlyActive, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<ExtraOrDeductionTypeDto> CreateAsync(CreateExtraOrDeductionTypeRequest request, CancellationToken ct = default)
    {
        var entity = new ExtraOrDeductionType
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            IsActive = request.IsActive,
            IsExtra = request.IsExtra,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(entity);
    }

    public async Task<ExtraOrDeductionTypeDto> UpdateAsync(Guid id, UpdateExtraOrDeductionTypeRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionTypeNotFound);

        entity.Title = request.Title.Trim();
        entity.IsActive = request.IsActive;
        entity.IsExtra = request.IsExtra;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(MessageKeys.ExtraOrDeductionTypeNotFound);

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private static ExtraOrDeductionTypeDto MapToDto(ExtraOrDeductionType e) => new()
    {
        Id = e.Id,
        Title = e.Title,
        IsActive = e.IsActive,
        IsExtra = e.IsExtra,
        CreatedAt = e.CreatedAt
    };
}