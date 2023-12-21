using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<int> CreateCategoryAsync(Category category);
    Task<int> UpdateCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesAsync(int? clientId);
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<int> DeleteCategoryAsync(Category category);
    Task<bool> DoesCategoryExistAsync(string name);
}
