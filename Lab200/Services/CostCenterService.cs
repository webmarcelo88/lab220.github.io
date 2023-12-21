using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class CostCenterService : ICostCenterService
{
    private readonly ICostCenterRepository _costCenterRepository;

    public CostCenterService(ICostCenterRepository costCenterRepository)
    {
        _costCenterRepository = costCenterRepository;
    }

    public async Task<int> CreateCostCenterAsync(CostCenter costCenter)
    {
        return await _costCenterRepository.CreateCostCenterAsync(costCenter);
    }

    public async Task<int> DeleteCostCenterAsync(CostCenter costCenter)
    {
        costCenter.IsDeleted = true;
        return await _costCenterRepository.DeleteCostCenterAsync(costCenter);
    }

    public async Task<bool> DoesCostCenterExistsAsync(string name)
    {
        var exists = await _costCenterRepository.DoesCostCenterExistsAsync($"{name}");
        return exists;
    }

    public async Task<List<CostCenter>> GetCostCentersByClientAsync(int? clientId)
    {
        return await _costCenterRepository.GetCostCentersByClientAsync(clientId);
    }

    public async Task<CostCenter> GetCostCentersById(int? costCenterId)
    {
        if (costCenterId == null || costCenterId == default(int))
            return null!;

        return await _costCenterRepository.GetCostCentersByIdAsync(costCenterId);
    }

    public async Task<int> UpdateCostCenterAsync(CostCenter costCenter)
    {
       return await _costCenterRepository.UpdateCostCenterAsync(costCenter);
    }
}
