using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IEmployeeItemsRepository
{
    Task<int> AddNewEmployeeItem(EmployeeItems item);
    Task<List<EmployeeItems>> GetEmployeeItemsListAsync(int? clientId, int employeeId);
}
