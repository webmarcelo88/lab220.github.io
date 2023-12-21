using Lab200.Entities;

namespace Lab200.Interfaces.Services;
public interface ICategoryService
{
    Task<int> CreateCategoryAsync(Category category);
    Task<int> UpdateCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesAsync(int? clientId);
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<int> DeleteCategoryAsync(Category category);
}