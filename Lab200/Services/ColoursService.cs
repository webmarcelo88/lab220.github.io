using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class ColoursService : IColoursService
{
    private readonly IColoursRepository _coloursRepository;

    public ColoursService(IColoursRepository coloursRepository)
    {
        _coloursRepository = coloursRepository;
    }

    public async Task<int> CreateColourAsync(Colours colour)
    {
        var exists = await _coloursRepository.DoesColourExistAsync(colour.Name, colour.ClientId);

        if (exists)
        {
            return 0;
        }

        var created = await _coloursRepository.CreateColourAsync(colour);
        return created;
    }

    public async Task<int> DeleteColourAsync(Colours colour)
    {
        colour.IsDeleted = true;

        var isDeleted = await _coloursRepository.DeleteColourAsync(colour);
        return isDeleted;
    }

    public async Task<List<Colours>> GetAllColoursAsync(int? clientId)
    {
        var colours = await _coloursRepository.GetAllColoursAsync(clientId);
        return colours;
    }

    public async Task<Colours?> GetColourByIdAsync(int colourId)
    {
        var colour = await _coloursRepository.GetColourByIdAsync(colourId);
        return colour;
    }

    public async Task<int> UpdateColourAsync(Colours colour)
    {
        var dbColour = await GetColourByIdAsync(colour.Id);

        if (dbColour == null)
        {
            return 0;
        }

        var updateColour = await _coloursRepository.UpdateColourAsync(colour);

        return updateColour;
    }
}
