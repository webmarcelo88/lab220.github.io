using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IEmployeeItemsService
{
    Task<int> AddNewEmployeeItem(EmployeeItems item);
    Task<List<EmployeeItems>>? GetEmployeeItemsListAsync(int? clientId, int employeeId);
}
