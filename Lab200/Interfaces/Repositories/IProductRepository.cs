using System;
using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IProductRepository
{
    Task<int> CreateProductAsync(Product product);
    Task<int> UpdateProductAsync(Product product);
    Task<List<Product>> GetProductsByClientAsync(int? clientId);
    Task<Product> GetProductByIdAsync(int productId);
    Task<int> DeleteProductAsync(Product product);
    Task<bool> DoesProductExistsAsync(int categoryId, int? clientId, string name);
}

