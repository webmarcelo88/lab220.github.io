using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface ICostCenterRepository
{
    Task<int> CreateCostCenterAsync(CostCenter costCenter);
    Task<int> UpdateCostCenterAsync(CostCenter costCenter);
    Task<List<CostCenter>> GetCostCentersByClientAsync(int? clientId);
    Task<CostCenter> GetCostCentersByIdAsync(int? costCenterId);
    Task<int> DeleteCostCenterAsync(CostCenter costCenter);
    Task<bool> DoesCostCenterExistsAsync(string name);
}