using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface ISectorRepository
{
    Task<int> CreateSectorAsync(Sector sector);
    Task<int> UpdateSectorAsync(Sector sector);
    Task<List<Sector>> GetSectorByClientAndCostCenterAsync(int? clientId, int costCenterId);
    Task<List<Sector>> GetSectorByClientAsync(int? clientId);
    Task<Sector> GetSectorByIdAsync(int  sectorId);
    Task<int> DeleteSectorAsync(Sector sector);
    Task<bool> DoesSectorExistsAsync(string name, int costCenterId, int? clientId);
}
