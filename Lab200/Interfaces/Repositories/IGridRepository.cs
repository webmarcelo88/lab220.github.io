using System;
using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IGridRepository
{
    Task<int> CreateGridAsync(Grid grid);
    Task<int> UpdateGridAsync(Grid grid);
    Task<List<Grid>> GetGridsByClientAsync(int? clientId);
    Task<List<Grid>> GetGridsByClientAndProductIdAsync(int? clientId, int productId);
    Task<int> DeleteGridAsync(Grid grid);
    Task<bool> DoesGridExistsAsync(Grid grid);
}

