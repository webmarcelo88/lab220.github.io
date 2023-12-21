using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class PlantRepository : IPlantRepository
{
    private readonly LabContext _context;

    public PlantRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreatePlantAsync(Plant plant)
    {
        try
        {
            await _context.Plants.AddAsync(plant);
            var newPlant = await _context.SaveChangesAsync();
            return newPlant;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int> DeletePlantAsync(Plant plant)
    {
        _context.Update(plant);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesPlantExistsAsync(string name, int? clientId)
    {
        var plantExists = await _context.Plants
            .AsNoTracking()
            .Where(x => x.Name.ToUpper() == name.ToUpper() && (clientId == null || x.ClientId == clientId))
            .FirstOrDefaultAsync();

        return plantExists != null;
    }

    public async Task<List<Plant>> GetAllPlantsByClientAsync(int? clientId)
    {
        var plants = await _context.Plants.AsNoTracking().Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false).ToListAsync();
        return plants;
    }

    public async Task<Plant> GetPlantByIdAsync(int plantId)
    {
        var plant = await _context.Plants
            .Where(x => x.Id == plantId)
            .FirstOrDefaultAsync();

        return plant;
    }

    public async Task<int> UpdatePlantAsync(Plant plant)
    {
        var dbPlant = await _context.Plants
             .AsNoTracking()
             .Where(x => x.Id == plant.Id)
             .FirstOrDefaultAsync();

        if (dbPlant == null)
            return 0;

        _context.Update(plant);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
