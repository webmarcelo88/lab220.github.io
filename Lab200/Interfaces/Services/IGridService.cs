using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IGridService
{
    Task<int> CreateGridAsync(Grid grid);
    Task<int> UpdateGridAsync(Grid grid);
    Task<List<Grid>> GetGridsByClientAsync(int? clientId);
    Task<List<Grid>> GetGridsByClientAndProductIdAsync(int? clientId, int productId);
    Task<int> DeleteGridAsync(Grid grid);
    Task<bool> DoesGridExistsAsync(Grid grid);
}
