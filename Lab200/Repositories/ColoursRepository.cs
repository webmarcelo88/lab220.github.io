using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class ColoursRepository : IColoursRepository
{
    private readonly LabContext _context;

    public ColoursRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateColourAsync(Colours colour)
    {
        try
        {
            await _context.Colours.AddAsync(colour);
            var newColour = await _context.SaveChangesAsync();
            return newColour;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }

    public async Task<int> DeleteColourAsync(Colours colour)
    {
        _context.Update(colour);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesColourExistAsync(string name, int? clientId)
    {
        var colourExists = await _context.Colours
             .AsNoTracking()
             .Where(x => x.Name.ToUpper() == name.ToUpper() && (clientId == null || x.ClientId == clientId))
             .FirstOrDefaultAsync();

        return colourExists != null;
    }

    public async Task<List<Colours>> GetAllColoursAsync(int? clientId)
    {
        var colours = await _context.Colours
             .AsNoTracking()
             .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted != true)
             .ToListAsync();

        return colours;
    }

    public async Task<Colours?> GetColourByIdAsync(int colourId)
    {
        var colour = await _context.Colours
             .Where(x => x.Id == colourId)
             .FirstOrDefaultAsync();

        return colour;
    }

    public async Task<int> UpdateColourAsync(Colours colour)
    {
        var dbColour = await _context.Colours
            .AsNoTracking()
            .Where(x => x.Id == colour.Id)
            .FirstOrDefaultAsync();

        if (dbColour == null)
            return 0;

        _context.Update(colour);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
