using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IPlantService
{
    Task<int> CreatePlantAsync(Plant plant);
    Task<int> UpdatePlantAsync(Plant plant);
    Task<List<Plant>> GetAllPlantsByClientAsync(int? clientId);
    Task<Plant> GetPlantByIdAsync(int plantId);
    Task<int> DeletePlantAsync(Plant plant);
    Task<bool> DoesPlantExistsAsync(string name, int? clientId);
}
