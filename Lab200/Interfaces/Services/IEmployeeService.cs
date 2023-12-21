using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<int> AddNewEmployee(Employee employee);
    Task<int> DeleteEmployeeAsync(Employee employee);
    Task<int> UpdateEmployeeAsync(Employee employee);
}
