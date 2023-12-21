using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class PlantService : IPlantService
{
    private readonly IPlantRepository _plantRepository;

    public PlantService(IPlantRepository plantRepository)
    {
        _plantRepository = plantRepository;
    }

    public async Task<int> CreatePlantAsync(Plant plant)
    {
        return await _plantRepository.CreatePlantAsync(plant);
    }

    public async Task<int> DeletePlantAsync(Plant plant)
    {
        plant.IsDeleted = true;
        return await _plantRepository.DeletePlantAsync(plant);
    }

    public async Task<bool> DoesPlantExistsAsync(string name, int? clientId)
    {
        var exists = await _plantRepository.DoesPlantExistsAsync($"{name}", clientId);
        return exists;
    }

    public async Task<List<Plant>> GetAllPlantsByClientAsync(int? clientId)
    {
        return await _plantRepository.GetAllPlantsByClientAsync(clientId);
    }

    public async Task<Plant> GetPlantByIdAsync(int plantId)
    {
        if (plantId == default(int))
            return null!;

        return await _plantRepository.GetPlantByIdAsync(plantId);
    }

    public async Task<int> UpdatePlantAsync(Plant plant)
    {
        return await _plantRepository.UpdatePlantAsync(plant);
    }
}
