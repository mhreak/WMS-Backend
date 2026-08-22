// WMS.Application/Administrator/Contracts/Services/ContractStepService.cs
using WMS.Application.Administrator.Contracts.DTOs;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Application.Common.Exceptions;
using WMS.Application.Common.Interfaces;
using WMS.Application.Common.Localization;
using WMS.Domain.Entities.Contracts;

namespace WMS.Application.Administrator.Contracts.Services;

public class ContractStepService : IContractStepService
{
    private readonly IContractStepRepository _repo;
    private readonly IUnitOfWork _uow;

    public ContractStepService(IContractStepRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<List<ContractStepDto>> GetByContractIdAsync(Guid contractId, CancellationToken ct = default)
    {
        var list = await _repo.GetByContractIdAsync(contractId, ct);
        return list.Select(MapToDto).ToList();
    }

    public async Task<ContractStepDto> CreateAsync(CreateContractStepRequest request, CancellationToken ct = default)
    {
        if (request.FinishDate < request.StartDate)
            throw new BadRequestException(MessageKeys.InvalidContractDates);

        if (await _repo.ExistsAsync(request.ContractId, request.StepId, ct))
            throw new BadRequestException(MessageKeys.ContractStepAlreadyExists);

        var entity = new ContractStep
        {
            ContractId = request.ContractId,
            StepId = request.StepId,
            StartDate = request.StartDate,
            FinishDate = request.FinishDate
        };

        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var created = await _repo.GetAsync(request.ContractId, request.StepId, ct)
            ?? throw new NotFoundException(MessageKeys.ContractStepNotFound);

        return MapToDto(created);
    }

    public async Task<ContractStepDto> UpdateAsync(Guid contractId, Guid stepId, UpdateContractStepRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetAsync(contractId, stepId, ct)
            ?? throw new NotFoundException(MessageKeys.ContractStepNotFound);

        if (request.FinishDate < request.StartDate)
            throw new BadRequestException(MessageKeys.InvalidContractDates);

        entity.StartDate = request.StartDate;
        entity.FinishDate = request.FinishDate;

        await _repo.UpdateAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var updated = await _repo.GetAsync(contractId, stepId, ct)
            ?? throw new NotFoundException(MessageKeys.ContractStepNotFound);

        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid contractId, Guid stepId, CancellationToken ct = default)
    {
        var entity = await _repo.GetAsync(contractId, stepId, ct)
            ?? throw new NotFoundException(MessageKeys.ContractStepNotFound);

        await _repo.RemoveAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
    }

    /// <summary>
    /// جایگزینی کامل مراحل یک قرارداد
    /// </summary>
    public async Task<List<ContractStepDto>> SetStepsAsync(SetContractStepsRequest request, CancellationToken ct = default)
    {
        foreach (var step in request.Steps)
        {
            if (step.FinishDate < step.StartDate)
                throw new BadRequestException(MessageKeys.InvalidContractDates);
        }

        await _repo.RemoveByContractIdAsync(request.ContractId, ct);

        var entities = request.Steps
            .DistinctBy(x => x.StepId)
            .Select(x => new ContractStep
            {
                ContractId = request.ContractId,
                StepId = x.StepId,
                StartDate = x.StartDate,
                FinishDate = x.FinishDate
            })
            .ToList();

        if (entities.Count > 0)
            await _repo.AddRangeAsync(entities, ct);

        await _uow.SaveChangesAsync(ct);

        var list = await _repo.GetByContractIdAsync(request.ContractId, ct);
        return list.Select(MapToDto).ToList();
    }

    private static ContractStepDto MapToDto(ContractStep entity) => new()
    {
    ContractId = entity.ContractId,
    StepId = entity.StepId,
    StepTitle = entity.Step?.Title,
    StepOrder = entity.Step?.StepOrder,
    StartDate = entity.StartDate,
    FinishDate = entity.FinishDate
    };
}