using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface ICostCenterService
{
    Task<int> CreateCostCenterAsync(CostCenter costCenter);
    Task<int> UpdateCostCenterAsync(CostCenter costCenter);
    Task<CostCenter> GetCostCentersById(int? costCenterId);
    Task<int> DeleteCostCenterAsync(CostCenter costCenter);
    Task<bool> DoesCostCenterExistsAsync(string name);
    Task<List<CostCenter>> GetCostCentersByClientAsync(int? clientId);
}
