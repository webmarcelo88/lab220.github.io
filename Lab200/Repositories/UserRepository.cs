using Lab200.Context;
using Lab200.Entities;
using Lab200.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LabContext _context;

    public UserRepository(LabContext context)
    {
        _context = context;
    }

    public async Task<int> CreateUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            var newUser = await _context.SaveChangesAsync();
            return newUser;
        }
        catch (Exception ex)
        {
            return 0;
        }

    }

    public async Task<int> DeleteUserAsync(User user)
    {
        _context.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsersAsync(int? clientId)
    {
        var users = await _context.Users
            .AsNoTracking()
            .Where(x => (clientId == null || x.ClientId == clientId))
            .ToListAsync();

        return users;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(x => x.Email.ToUpper() == email.ToUpper()).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(x => x.Email.ToUpper() == email.ToUpper()
             && x.Password == password
             && x.IsDeleted == false)
            .FirstOrDefaultAsync();
    }

    public async Task<int> UpdateUserAsync(User user)
    {
        var dbUser = await _context.Users.AsNoTracking().Where(x=>x.Id == user.Id).FirstOrDefaultAsync();
        if(dbUser == null)
        {
            return 0;
        }

        _context.Update(user);
        var updated = await _context.SaveChangesAsync();
        return updated;
        
    }
}
