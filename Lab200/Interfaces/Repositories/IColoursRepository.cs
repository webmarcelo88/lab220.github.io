using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IColoursRepository
{
    Task<int> CreateColourAsync(Colours colour);
    Task<int> UpdateColourAsync(Colours colour);
    Task<List<Colours>> GetAllColoursAsync(int? clientId);
    Task<Colours?> GetColourByIdAsync(int colourId);
    Task<int> DeleteColourAsync(Colours colour);
    Task<bool> DoesColourExistAsync(string name, int? clientId);
}
