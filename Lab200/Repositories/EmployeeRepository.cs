using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Repositories;
using Lab200.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly LabContext _context;
    private readonly ISessionState _sessionState;

    public EmployeeRepository(LabContext context, ISessionState sessionState)
    {
        _context = context;
        _sessionState = sessionState;
    }

    public async Task<int> AddNewEmployee(Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            var newEmployee = await _context.SaveChangesAsync();
            return newEmployee;
        }
        catch (Exception ex)
        {
            return 0;
        }

    }

    public async Task<int> DeleteEmployeeAsync(Employee employee)
    {
        try
        {
            _context.Employees.Remove(employee);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees.AsNoTracking().Where(x => _sessionState.User.ClientId == null || x.ClientId == _sessionState.User.ClientId).ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
            var employee = await _context.Employees
                        .Where(x => x.Id == id).FirstOrDefaultAsync();
            return employee;

    }

    public async Task<int> UpdateEmployeeAsync(Employee employee)
    {
        _context.Update(employee);
        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
//.Include(x => x.Client)
//.Include(x => x.Plant)
//.Include(x => x.CostCenter)
//.Include(x => x.Sector)
//.Include(x => x.Function)