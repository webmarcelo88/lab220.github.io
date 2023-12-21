using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> CreateProductAsync(Product product)
    {
        return await _productRepository.CreateProductAsync(product);
    }

    public async Task<int> DeleteProductAsync(Product product)
    {
        return await _productRepository.DeleteProductAsync(product);
    }

    public async Task<bool> DoesProductExistsAsync(int categoryId, int? clientId, string name)
    {
        return await _productRepository.DoesProductExistsAsync(categoryId, clientId, name);
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetProductByIdAsync(productId);
    }

    public async Task<List<Product>> GetProductsByClientAsync(int? clientId)
    {
        return await _productRepository.GetProductsByClientAsync(clientId);
    }

    public async Task<int> UpdateProductAsync(Product product)
    {
        return await _productRepository.UpdateProductAsync(product);
    }
}
