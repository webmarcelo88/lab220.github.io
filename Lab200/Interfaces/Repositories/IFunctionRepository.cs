using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IFunctionRepository
{
    Task<List<Function>> GetFunctionsByClientAndCostCenterAsync(int? clientId, int costCenterId);
    Task<Function> GetFunctionByIdAsync(int functionId);
    Task<int> AddNewFunctionAsync(Function function);
    Task<int> DeleteFunctionAsync(Function function);
    Task<int> UpdateFunctionAsync(Function function);
}
