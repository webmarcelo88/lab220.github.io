using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LabContext _context;

    public ProductRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateProductAsync(Product product)
    {
        try
        {
            var dbProduct = await DoesProductExistsAsync(product.CategoryId, product.ClientId, product.Name);
            if (dbProduct)
                return -1;

            await _context.Products.AddAsync(product);
            var newProduct = await _context.SaveChangesAsync();
            return newProduct;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<int> DeleteProductAsync(Product product)
    {
        product.IsDeleted = true;
        _context.Update(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesProductExistsAsync(int categoryId, int? clientId, string name)
    {
        var exists = await _context.Products
            .AsNoTracking()
            .Where(x => x.CategoryId == categoryId && (clientId == null || x.ClientId == clientId) && x.Name.ToUpper() == name.ToUpper())
            .FirstOrDefaultAsync();

        return exists != null;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _context.Products
             .Include(x => x.Category)
             .Include(x => x.Plant)
             .Include(x => x.ProductType)
             .Where(x => x.Id == productId)
             .FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetProductsByClientAsync(int? clientId)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Include(x=>x.Category)
            .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false)
            .ToListAsync();

        return products;
    }

    public async Task<int> UpdateProductAsync(Product product)
    {
        var dbProduct = await _context.Products
            .AsNoTracking()
            .Where(x => x.Id == product.Id).FirstOrDefaultAsync();

        if(dbProduct == null)
            return 0;

        _context.Update(product);
        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}

