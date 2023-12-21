using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<int> AddNewEmployee(Employee employee)
    {
       return await _employeeRepository.AddNewEmployee(employee);
    }

    public async Task<int> DeleteEmployeeAsync(Employee employee)
    {
        return await _employeeRepository.DeleteEmployeeAsync(employee);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        return await _employeeRepository.GetAllAsync();
    }

    public async Task<int> UpdateEmployeeAsync(Employee employee)
    {
        var isUpdated = await _employeeRepository.UpdateEmployeeAsync(employee);
        return isUpdated;
    }
}
