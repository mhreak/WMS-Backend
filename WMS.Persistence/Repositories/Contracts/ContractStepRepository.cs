// WMS.Persistence/Repositories/Contracts/ContractStepRepository.cs
using Microsoft.EntityFrameworkCore;
using WMS.Application.Administrator.Contracts.Interfaces;
using WMS.Domain.Entities.Contracts;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories.Contracts;

public class ContractStepRepository : IContractStepRepository
{
    private readonly AppDbContext _db;

    public ContractStepRepository(AppDbContext db) => _db = db;

    public async Task<ContractStep?> GetAsync(Guid contractId, Guid stepId, CancellationToken ct = default)
    {
        return await _db.Contract_ContractTypeStep
            .Include(x => x.Step)
            .FirstOrDefaultAsync(x => x.ContractId == contractId && x.StepId == stepId, ct);
    }

    public async Task<List<ContractStep>> GetByContractIdAsync(Guid contractId, CancellationToken ct = default)
    {
        return await _db.Contract_ContractTypeStep
            .AsNoTracking()
            .Include(x => x.Step)
            .Where(x => x.ContractId == contractId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(ContractStep entity, CancellationToken ct = default)
    {
        await _db.Contract_ContractTypeStep.AddAsync(entity, ct);
    }

    public async Task AddRangeAsync(IEnumerable<ContractStep> entities, CancellationToken ct = default)
    {
        await _db.Contract_ContractTypeStep.AddRangeAsync(entities, ct);
    }

    public Task UpdateAsync(ContractStep entity, CancellationToken ct = default)
    {
        _db.Contract_ContractTypeStep.Update(entity);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(ContractStep entity, CancellationToken ct = default)
    {
        _db.Contract_ContractTypeStep.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task RemoveByContractIdAsync(Guid contractId, CancellationToken ct = default)
    {
        var items = await _db.Contract_ContractTypeStep
            .Where(x => x.ContractId == contractId)
            .ToListAsync(ct);

        _db.Contract_ContractTypeStep.RemoveRange(items);
    }

    public async Task<bool> ExistsAsync(Guid contractId, Guid stepId, CancellationToken ct = default)
    {
        return await _db.Contract_ContractTypeStep
            .AnyAsync(x => x.ContractId == contractId && x.StepId == stepId, ct);
    }
}