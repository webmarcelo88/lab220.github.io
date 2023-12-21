using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class ThemeService : IThemeService
{
    private readonly IThemeRepository _themeRepository;

    public ThemeService(IThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public async Task<int> CreateThemeAsync(Theme theme)
    {
        return await _themeRepository.CreateThemeAsync(theme);
    }

    public async Task<int> DeleteThemeAsync(Theme theme)
    {
       theme.IsDeleted = true;
       return await _themeRepository.DeleteThemeAsync(theme);
    }

    public async Task<bool> DoesThemeExistsAsync(int? clientId, string? themeName)
    {
        return await _themeRepository.DoesThemeExistsAsync(clientId, themeName);
    }

    public async Task<List<Theme>> GetAllThemesAsync(int? clientId)
    {
        return await _themeRepository.GetAllThemesAsync(clientId);
    }

    public async Task<Theme> GetThemeByIdAsync(int id)
    {
        return await _themeRepository.GetThemeByIdAsync(id);
    }

    public async Task<int> UpdateThemeAsync(Theme theme)
    {
        return await _themeRepository.UpdateThemeAsync(theme);
    }
}
