using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly LabContext _context;

    public ItemRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetItemsByClientAndSector(int sectorId, int? clientId)
    {
        var itens = await _context.Items
             .AsNoTracking()
             .Include(x => x.Product)
             .ThenInclude(x => x.Category)
             .Include(x => x.Client)
             .ThenInclude(x => x.Sectors)
             .Include(x => x.VirtualMachine)
             .Include(x => x.Size)
             .Include(x => x.Color)
             .AsNoTracking()
             .Where(x => (clientId == null || x.ClientId == clientId))
             .ToListAsync();

        //return itens;

        return itens.Where(x => x.Client.Sectors != null && x.Client.Sectors.Any(y => y.Id == sectorId)).ToList();
    }
}