using Lab200.Entities;
using Lab200.Helpers;
using Lab200.Interfaces.Repositories;
using Lab200.Interfaces.Services;

namespace Lab200.Services;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> CreateUserAsync(User user)
    {
        var userExists = await GetUserByEmailAsync(user.Email);
        if (userExists != null)
        {
            return 0;
        }

        user.Password = Encoders.Encode(user.Password);

        var addUser = await _userRepository.CreateUserAsync(user);
        return addUser;
    }

    public async Task<int> DeleteUserAsync(User user)
    {
        user.IsActive = false;
        user.IsDeleted = true;

        var IsDeleted = await _userRepository.DeleteUserAsync(user);
        return IsDeleted;
    }

    public async Task<List<User>> GetAllUsersAsync(int? clientId)
    {
        var users = await _userRepository.GetAllUsersAsync(clientId);
        return users;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        return await _userRepository.LoginAsync(email, Encoders.Encode(password));
    }

    public async Task<int> UpdateUserAsync(User user)
    {
        var dbUser = await GetUserByIdAsync(user.Id);

        if (dbUser != null)
        {
            if (dbUser.Password != Encoders.Encode(user.Password))
                user.Password = Encoders.Encode(user.Password);
            else
                user.Password = dbUser.Password;
        }

        var updateUser = await _userRepository.UpdateUserAsync(user);

        return updateUser;
    }
}
