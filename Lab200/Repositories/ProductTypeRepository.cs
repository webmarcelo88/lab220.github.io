using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly LabContext _context;

    public ProductTypeRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<List<ProductType>> GetProductTypesByClientIdAsync(int? clientId)
    {
        var types = await _context.ProductsTypes
            .AsNoTracking()
            .Where(x => (clientId == null || x.ClientId == clientId))
            .ToListAsync();

        return types;
    }
}
