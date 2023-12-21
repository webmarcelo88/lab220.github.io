using Lab200.Entities;

namespace Lab200.Interfaces.Repositories;

public interface IUserRepository
{
    Task<int> CreateUserAsync(User user);
    Task<int> UpdateUserAsync(User user);
    Task<List<User>> GetAllUsersAsync(int? clientId);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<User?> LoginAsync(string email, string password);
    Task<int> DeleteUserAsync(User user);
}
