using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;

    public ProductTypeService(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }

    public async Task<List<ProductType>> GetProductTypesByClientIdAsync(int? clientId)
    {
        return await _productTypeRepository.GetProductTypesByClientIdAsync(clientId);
    }
}
