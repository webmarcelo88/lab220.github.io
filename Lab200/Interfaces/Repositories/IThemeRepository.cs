using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IThemeRepository
{
    Task<int> CreateThemeAsync(Theme theme);
    Task<int> UpdateThemeAsync(Theme theme);
    Task<List<Theme>> GetAllThemesAsync(int? clientId);
    Task<Theme> GetThemeByIdAsync(int id);
    Task<int> DeleteThemeAsync(Theme theme);
    Task<bool> DoesThemeExistsAsync(int? clientId, string? themeName);
}
