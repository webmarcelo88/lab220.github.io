using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class SectorService : ISectorService
{
    private readonly ISectorRepository _sectorRepository;

    public SectorService(ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<int> CreateSectorAsync(Sector sector)
    {
        return await _sectorRepository.CreateSectorAsync(sector);
    }

    public async Task<int> DeleteSectorAsync(Sector sector)
    {
        sector.IsDeleted = true;
        return await _sectorRepository.DeleteSectorAsync(sector);
    }

    public async Task<bool> DoesSectorExistsAsync(string name, int costCenterId, int? clientId)
    {
        var exists = await _sectorRepository.DoesSectorExistsAsync(name, costCenterId, clientId);
        return exists;
    }

    public async Task<List<Sector>> GetSectorByClientAndCostCenterAsync(int? clientId, int costCenterId)
    {
        return await _sectorRepository.GetSectorByClientAndCostCenterAsync(clientId, costCenterId);
    }

    public async Task<List<Sector>> GetSectorByClientAsync(int? clientId)
    {
        return await _sectorRepository.GetSectorByClientAsync(clientId);
    }

    public async Task<Sector> GetSectorByIdAsync(int sectorId)
    {
        if (sectorId == default(int))
            return null!;

        return await _sectorRepository.GetSectorByIdAsync(sectorId);
    }

    public async Task<int> UpdateSectorAsync(Sector sector)
    {
        return await _sectorRepository.UpdateSectorAsync(sector);
    }
}
