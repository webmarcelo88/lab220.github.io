using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IScaleService
{
    Task<int> CreateScaleAsync(Scale scale);
    Task<int> UpdateScaleAsync(Scale scale);
    Task<List<Scale>> GetAllScalesAsync(int? clientId);
    Task<Scale?> GetScaleByIdAsync(int scaleId);
    Task<int> DeleteScaleAsync(Scale scale);
}
