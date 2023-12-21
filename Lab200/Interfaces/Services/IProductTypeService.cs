using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IProductTypeService
{
    Task<List<ProductType>> GetProductTypesByClientIdAsync(int? clientId);

}
