using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;
using Lab200.Repositories;

namespace Lab200.Services;

public class FunctionService : IFunctionService
{
    private readonly IFunctionRepository _functionRepository;

    public FunctionService(IFunctionRepository functionRepository)
    {
        _functionRepository = functionRepository;
    }

    public async Task<int> AddNewFunctionAsync(Function function)
    {
        return await _functionRepository.AddNewFunctionAsync(function);
    }

    public async Task<int> DeleteFunctionAsync(Function function)
    {
        function.IsDeleted = true;
        return await _functionRepository.DeleteFunctionAsync(function);
    }

    public async Task<Function> GetFunctionByIdAsync(int functionId)
    {
        if (functionId == default)
            return null!;

        return await _functionRepository.GetFunctionByIdAsync(functionId);
    }

    public async Task<List<Function>> GetFunctionsByClientAndCostCenterAsync(int? clientId, int costCenterId)
    {
        return await _functionRepository.GetFunctionsByClientAndCostCenterAsync(clientId, costCenterId);
    }

    public async Task<int> UpdateFunctionAsync(Function function)
    {
        return await _functionRepository.UpdateFunctionAsync(function);
    }
}
