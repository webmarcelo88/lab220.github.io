using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IScaleRepository
{
    Task<int> CreateScaleAsync(Scale scale);
    Task<int> UpdateScaleAsync(Scale scale);
    Task<List<Scale>> GetAllScalesAsync(int? clientId);
    Task<Scale?> GetScaleByIdAsync(int scaleId);
    Task<int> DeleteScaleAsync(Scale scale);
    Task<bool> DoesScaleExist(string name);
}
