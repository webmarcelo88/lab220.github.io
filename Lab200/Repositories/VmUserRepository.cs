using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;
public class VmUserRepository : IVmUserRepository
{
    private readonly LabContext _context;

    public VmUserRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateVmUserAsync(VmUser vmUser)
    {
        try
        {
            await _context.VmUsers.AddAsync(vmUser);
            var newVmUser = await _context.SaveChangesAsync();
            return newVmUser;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<int> DeleteVmUserAsync(VmUser vmUser)
    {
        _context.Update(vmUser);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesUserExist(string login)
    {
        var userExists = await _context.VmUsers
        .AsNoTracking()
        .Where(x => x.Login.ToUpper() == login.ToUpper())
        .FirstOrDefaultAsync();

        return userExists != null;
    }

    public async Task<List<VmUser>> GetAllVmUsersAsync(int? clientId)
    {
        var vmUsers = await _context.VmUsers
            .AsNoTracking()
            .Where(x => (clientId == null || x.ClientId == clientId) && x.IsDeleted == false)
            .ToListAsync();

        return vmUsers;
    }

    public async Task<VmUser?> GetVmUserByIdAsync(int vmUserId)
    {
        var vmUser = await _context.VmUsers
            .Where(x => x.Id == vmUserId)
            .FirstOrDefaultAsync();

        return vmUser;
    }

    public async Task<int> UpdateVmUserAsync(VmUser user)
    {
        var dbVmUser = await _context.VmUsers
            .AsNoTracking()
            .Where(x => x.Id == user.Id)
            .FirstOrDefaultAsync();

        if (dbVmUser == null)
            return 0;
        
        _context.Update(user);

        var updated = await _context.SaveChangesAsync();
        return updated;
    }
}