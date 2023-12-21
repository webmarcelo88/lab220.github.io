using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IItemRepository
{
    Task<List<Item>>GetItemsByClientAndSector(int sectorId, int? clientId);
}
