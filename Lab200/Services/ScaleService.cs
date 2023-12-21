using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;
using Lab200.Repositories;

namespace Lab200.Services;

public class ScaleService : IScaleService
{
    private readonly IScaleRepository _scaleRepository;

    public ScaleService(IScaleRepository scaleRepository)
    {
        _scaleRepository = scaleRepository;
    }

    public async Task<int> CreateScaleAsync(Scale scale)
    {
        var exists = await _scaleRepository.DoesScaleExist(scale.Name);

        if (exists)
        {
            return 0;
        }

        var created = await _scaleRepository.CreateScaleAsync(scale);
        return created;
    }

    public async Task<int> DeleteScaleAsync(Scale scale)
    {
       scale.IsDeleted = true;

        var isDeleted = await _scaleRepository.DeleteScaleAsync(scale);
        return isDeleted;
    }

    public async Task<List<Scale>> GetAllScalesAsync(int? clientId)
    {
        var scales = await _scaleRepository.GetAllScalesAsync(clientId);
        return scales;
    }

    public async Task<Scale?> GetScaleByIdAsync(int scaleId)
    {
        var scale = await _scaleRepository.GetScaleByIdAsync(scaleId);
        return scale;
    }

    public async Task<int> UpdateScaleAsync(Scale scale)
    {
        var dbScale = await GetScaleByIdAsync(scale.Id);

        if (dbScale == null)
        {
            return 0;
        }

        var updateScale = await _scaleRepository.UpdateScaleAsync(scale);

        return updateScale;
    }
}
