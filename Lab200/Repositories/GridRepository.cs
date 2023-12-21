using System;
using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class GridRepository : IGridRepository
{
    private readonly LabContext _context;

    public GridRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateGridAsync(Grid grid)
    {
        try
        {
            await _context.Grids.AddAsync(grid);
            var newGrid = await _context.SaveChangesAsync();
            return newGrid;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<int> DeleteGridAsync(Grid grid)
    {
        _context.Remove(grid);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesGridExistsAsync(Grid grid)
    {
        var exists = await _context.Grids
            .AsNoTracking()
            .Where(x => x.ClientId == grid.ClientId
                    && x.ProductId == grid.ProductId
                    && x.Sku == grid.Sku
                    && x.ScaleId == grid.ScaleId)
            .FirstOrDefaultAsync();

        return exists != null;
    }

    public async Task<List<Grid>> GetGridsByClientAndProductIdAsync(int? clientId, int productId)
    {
        var grids = await _context.Grids
             .AsNoTracking()
             .Where(x => (clientId == null || x.ClientId == clientId) && x.ProductId == productId)
             .ToListAsync();
        return grids;
    }

    public async Task<List<Grid>> GetGridsByClientAsync(int? clientId)
    {
        var grids = await _context.Grids
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Scale)
            .Include(x => x.Colors)
            .Where(x => (clientId == null || x.ClientId == clientId) && x.Sku != null && x.Product.IsDeleted != null && !x.Product.IsDeleted.Value)
            .OrderBy(x => x.Product.Name)
            .ToListAsync();

        return grids;
    }

    public async Task<int> UpdateGridAsync(Grid grid)
    {
        _context.Update(grid);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}

