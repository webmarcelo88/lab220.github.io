using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class FunctionRepository : IFunctionRepository
{
    private readonly LabContext _context;

    public FunctionRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> AddNewFunctionAsync(Function function)
    {
        try
        {
            await _context.Functions
                .AddAsync(function);

            var newFunction = await _context.SaveChangesAsync();
            return newFunction;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<int> DeleteFunctionAsync(Function function)
    {
        try
        {
            _context.Functions.Remove(function);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<Function> GetFunctionByIdAsync(int functionId)
    {
        var function = await _context.Functions
            .Include(x => x.CostCenter)
            .Where(x => x.Id == functionId)
            .FirstOrDefaultAsync();

        return function;
    }

    public async Task<List<Function>> GetFunctionsByClientAndCostCenterAsync(int? clientId, int costCenterId)
    {
        return await _context.Functions
            .AsNoTracking()
            .Include(x => x.CostCenter)
            .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<int> UpdateFunctionAsync(Function function)
    {
        _context.Update(function);
        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
