using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class SectorRepository : ISectorRepository
{
    private readonly LabContext _context;

    public SectorRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateSectorAsync(Sector sector)
    {
        try
        {
            await _context.Sectors.AddAsync(sector);
            var newSector = await _context.SaveChangesAsync();
            return newSector;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<int> DeleteSectorAsync(Sector sector)
    {
       _context.Update(sector);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesSectorExistsAsync(string name, int costCenterId, int? clientId)
    {
        var sectorExists = await _context.Sectors
            .AsNoTracking()
            .Where(x => x.Name.ToUpper() == name.ToUpper() && x.CostCenterId == costCenterId
                     && (clientId == null || x.ClientId == clientId))
            .FirstOrDefaultAsync();

        return sectorExists != null;
    }

    public async Task<List<Sector>> GetSectorByClientAndCostCenterAsync(int? clientId, int costCenterId)
    {
        return await _context.Sectors
            .AsNoTracking()
            .Include(x=>x.CostCenter)
            .Where(x => (clientId == null || x.ClientId == clientId) && x.CostCenterId == costCenterId && x.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<List<Sector>> GetSectorByClientAsync(int? clientId)
    {
        return await _context.Sectors
            .AsNoTracking()
            .Include(x => x.CostCenter)
            .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<Sector> GetSectorByIdAsync(int sectorId)
    {
        var sector = await _context.Sectors
            .Include(x => x.CostCenter)
            .Where(x=>x.Id == sectorId)
            .FirstOrDefaultAsync();

        return sector;
    }

    public async Task<int> UpdateSectorAsync(Sector sector)
    {
        var dbSector = await _context.Sectors
            .AsNoTracking()
            .Where(x=>x.Id == sector.Id)
            .FirstOrDefaultAsync();

        if(dbSector == null)
            return 0;

        _context.Update(sector);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
