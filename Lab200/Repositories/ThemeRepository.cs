using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class ThemeRepository : IThemeRepository
{
    private readonly LabContext _context;

    public ThemeRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateThemeAsync(Theme theme)
    {
        try
        {
            var dbTheme = await DoesThemeExistsAsync(theme.ClientId, theme.Name);
            if (dbTheme)
                return -1;

            await _context.Themes.AddAsync(theme);
            var newTheme = await _context.SaveChangesAsync();
            return newTheme;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<int> DeleteThemeAsync(Theme theme)
    {
        _context.Update(theme);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesThemeExistsAsync(int? clientId, string? themeName = null)
    {
        var dbTheme = await _context.Themes
            .AsNoTracking()
            .Where(theme => (clientId == null || theme.ClientId == clientId) && (string.IsNullOrEmpty(themeName) || theme.Name.ToUpper() == themeName.ToUpper()))
            .FirstOrDefaultAsync();

        return dbTheme != null;
    }

    public async Task<List<Theme>> GetAllThemesAsync(int? clientId)
    {
        var themes = await _context.Themes
            .AsNoTracking()
            .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false)
            .ToListAsync();

        return themes;
    }

    public async Task<Theme> GetThemeByIdAsync(int id)
    {
        var theme = await _context.Themes
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        return theme;
    }

    public async Task<int> UpdateThemeAsync(Theme theme)
    {
        var dbTheme = await _context.Themes
            .AsNoTracking()
            .Where(x=>x.Id == theme.Id)
            .FirstOrDefaultAsync();

        if (dbTheme == null)
            return 0;

        _context.Update(theme);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
