using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IItemService
{
    Task<List<Item>> GetItemsByClientAndSector(int sectorId, int? clientId);
}
