using System;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class GridService : IGridService
{
    private readonly IGridRepository _gridRepository;

    public GridService(IGridRepository gridRepository)
    {
        _gridRepository = gridRepository;
    }

    public async Task<int> CreateGridAsync(Grid grid)
    {
        return await _gridRepository.CreateGridAsync(grid);
    }

    public async Task<int> DeleteGridAsync(Grid grid)
    {
        return await _gridRepository.DeleteGridAsync(grid);
    }

    public async Task<bool> DoesGridExistsAsync(Grid grid)
    {
        return await _gridRepository.DoesGridExistsAsync(grid);
    }

    public async Task<List<Grid>> GetGridsByClientAndProductIdAsync(int? clientId, int productId)
    {
        return await _gridRepository.GetGridsByClientAndProductIdAsync(clientId, productId);
    }

    public async Task<List<Grid>> GetGridsByClientAsync(int? clientId)
    {
        return await _gridRepository.GetGridsByClientAsync(clientId);
    }

    public async Task<int> UpdateGridAsync(Grid grid)
    {
        return await _gridRepository.UpdateGridAsync(grid);
    }
}

