using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class ScaleRepository : IScaleRepository
{
    private readonly LabContext _context;
    private readonly ISessionState _sessionState;

    public ScaleRepository(LabContext context, ISessionState sessionState)
    {
        _context = context;
        _sessionState = sessionState;
    }

    public async Task<int> CreateScaleAsync(Scale scale)
    {
        try
        {
            await _context.Scales.AddAsync(scale);
            var newScale = await _context.SaveChangesAsync();
            return newScale;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }

    public async Task<int> DeleteScaleAsync(Scale scale)
    {
        _context.Update(scale);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesScaleExist(string name)
    {
        var scaleExists = await _context.Scales
        .AsNoTracking()
        .Where(x => x.Name.ToUpper() == name.ToUpper() && (_sessionState.User.ClientId == null || x.ClientId == _sessionState.User.ClientId))
        .FirstOrDefaultAsync();

        return scaleExists != null;
    }

    public async Task<List<Scale>> GetAllScalesAsync(int? clientId)
    {
        var scales = await _context.Scales
            .AsNoTracking()
            .Where(x => (clientId == null || x.ClientId == clientId))
            .ToListAsync();

        return scales;
    }

    public async Task<Scale?> GetScaleByIdAsync(int scaleId)
    {
        var scale = await _context.Scales
            .Where(x => x.Id == scaleId)
            .FirstOrDefaultAsync();

        return scale;
    }

    public async Task<int> UpdateScaleAsync(Scale scale)
    {
        var dbScale = await _context.Scales
            .AsNoTracking()
            .Where(x => x.Id == scale.Id)
            .FirstOrDefaultAsync();

        if (dbScale == null)
            return 0;

        _context.Update(scale);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}
