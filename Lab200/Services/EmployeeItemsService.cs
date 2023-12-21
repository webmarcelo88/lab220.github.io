using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class EmployeeItemsService : IEmployeeItemsService
{
    private readonly IEmployeeItemsRepository _employeeItemsRepository;

    public EmployeeItemsService(IEmployeeItemsRepository employeeItemsRepository)
    {
        _employeeItemsRepository = employeeItemsRepository;
    }

    public async Task<int> AddNewEmployeeItem(EmployeeItems item)
    {
        return await _employeeItemsRepository.AddNewEmployeeItem(item);
    }

    public async Task<List<EmployeeItems>>? GetEmployeeItemsListAsync(int? clientId, int employeeId)
    {
        return await _employeeItemsRepository.GetEmployeeItemsListAsync(clientId, employeeId);
    }
}
