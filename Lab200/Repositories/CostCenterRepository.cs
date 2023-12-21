using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class CostCenterRepository : ICostCenterRepository
{
    private readonly LabContext _context;

    public CostCenterRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateCostCenterAsync(CostCenter costCenter)
    {
        try
        {
            await _context.CostCenters.AddAsync(costCenter);
            var newCostCenter = await _context.SaveChangesAsync();
            return newCostCenter;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<int> DeleteCostCenterAsync(CostCenter costCenter)
    {
        _context.Update(costCenter);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesCostCenterExistsAsync(string name)
    {
        var costCenterExists = await _context.CostCenters
            .AsNoTracking()
            .Where(x => x.Name.ToUpper() == name.ToUpper())
            .FirstOrDefaultAsync();

        return costCenterExists != null;
    }

    public async Task<List<CostCenter>> GetCostCentersByClientAsync(int? clientId)
    {
        var costCenters = await _context.CostCenters.AsNoTracking().Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false).ToListAsync();
        return costCenters;
    }

    public async Task<CostCenter> GetCostCentersByIdAsync(int? costCenterId)
    {
        var costCenter = await _context.CostCenters
            .Where(x=> x.Id == costCenterId)
            .FirstOrDefaultAsync();

        return costCenter;
    }

    public async Task<int> UpdateCostCenterAsync(CostCenter costCenter)
    {
        var dbCostCenter = await _context.CostCenters
            .AsNoTracking()
            .Where(x => x.Id == costCenter.Id)
            .FirstOrDefaultAsync();
        
        if(dbCostCenter == null)
            return 0;

        _context.Update(costCenter);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
