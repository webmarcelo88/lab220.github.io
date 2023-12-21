using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<List<Item>> GetItemsByClientAndSector(int sectorId, int? clientId)
    {
       return await _itemRepository.GetItemsByClientAndSector(sectorId, clientId);
    }
}
