using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class EmployeeItemsRepository : IEmployeeItemsRepository
{
    private readonly LabContext _context;

    public EmployeeItemsRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> AddNewEmployeeItem(EmployeeItems item)
    {
        try
        {
            await _context.EmployeeItems.AddAsync(item);
            var newEmployeeItem = await _context.SaveChangesAsync();
            return newEmployeeItem;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<List<EmployeeItems>> GetEmployeeItemsListAsync(int? clientId, int employeeId)
    {
        return await _context.EmployeeItems
            .AsNoTracking()
            .Include(x=>x.Product)
            .ThenInclude(x=>x.Category)
            .Include(x=>x.Scale)
            .Include(x=>x.Colors)
            .Where(x=> (clientId == null || x.ClientId == clientId) && x.EmployeeId == employeeId && (x.Deleted == null || !x.Deleted.Value))
            .ToListAsync();
    }
}
