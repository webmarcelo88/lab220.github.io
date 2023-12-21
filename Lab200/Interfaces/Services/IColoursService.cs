using Lab200.Entities;

namespace Lab200.Interfaces.Services;

public interface IColoursService
{
    Task<int> CreateColourAsync(Colours colour);
    Task<int> UpdateColourAsync(Colours colour);
    Task<List<Colours>> GetAllColoursAsync(int? clientId);
    Task<Colours?> GetColourByIdAsync(int colourId);
    Task<int> DeleteColourAsync(Colours colour);
}
