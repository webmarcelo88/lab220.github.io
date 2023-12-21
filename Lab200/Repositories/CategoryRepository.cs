using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Repositories;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly LabContext _context;

    private readonly ISessionState _sessionState;

    public CategoryRepository(LabContext context, ISessionState sessionState)
    {
        _context = context;
        _sessionState = sessionState;
    }

    public async Task<int> CreateCategoryAsync(Category category)
    {
        try
        {
            await _context.Categories.AddAsync(category);
            var newCategory = await _context.SaveChangesAsync();
            return newCategory;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<int> DeleteCategoryAsync(Category category)
    {
        _context.Update(category);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesCategoryExistAsync(string name)
    {
        var categoryExists = await _context.Categories
        .AsNoTracking()
        .Where(x => x.Name.ToUpper() == name.ToUpper() && (_sessionState.User.ClientId == null || x.ClientId == _sessionState.User.ClientId))
        .FirstOrDefaultAsync();

        return categoryExists != null;
    }

    public async Task<List<Category>> GetAllCategoriesAsync(int? clientId)
    {
        var categories = await _context.Categories
         .AsNoTracking()
         .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted != true)
         .ToListAsync();

        return categories;
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _context.Categories
            .Where(x => x.Id == categoryId)
            .FirstOrDefaultAsync();

        return category;
    }

    public async Task<int> UpdateCategoryAsync(Category category)
    {
        var dbCategory = await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == category.Id)
            .FirstOrDefaultAsync();

        if (dbCategory == null)
            return 0;

        _context.Update(category);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
