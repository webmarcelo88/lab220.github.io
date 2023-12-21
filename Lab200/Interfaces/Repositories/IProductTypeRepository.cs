using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IProductTypeRepository
{
    Task<List<ProductType>>GetProductTypesByClientIdAsync(int? clientId);
}
