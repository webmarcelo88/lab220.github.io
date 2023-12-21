using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int id);
    Task<int> AddNewEmployee(Employee employee);
    Task<int> DeleteEmployeeAsync(Employee employee);
    Task<int> UpdateEmployeeAsync(Employee employee);
}
