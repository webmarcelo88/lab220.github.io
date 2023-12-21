using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<int> CreateCategoryAsync(Category category)
    {
        var exists = await _categoryRepository.DoesCategoryExistAsync(category.Name);

        if(exists)
        {
            return 0;
        }

        var created = await _categoryRepository.CreateCategoryAsync(category);
        return created;
    }

    public async Task<int> DeleteCategoryAsync(Category category)
    {
       category.IsDeleted = true;
       
        var isDeleted = await _categoryRepository.DeleteCategoryAsync(category);
        return isDeleted;
    }

    public async Task<List<Category>> GetAllCategoriesAsync(int? clientId)
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync(clientId);
        return categories;
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
        return category;
    }

    public async Task<int> UpdateCategoryAsync(Category category)
    {
        var dbCategory = await GetCategoryByIdAsync(category.Id);

        if (dbCategory == null)
        {
            return 0;
        }

        var updateUser = await _categoryRepository.UpdateCategoryAsync(category);

        return updateUser;
    }
}
