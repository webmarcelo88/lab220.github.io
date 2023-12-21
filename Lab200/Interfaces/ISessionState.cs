using Lab200.Entities;

namespace Lab200.Interfaces;

public interface ISessionState
{
    User? User { get; }
    Timer Timer { get; }
    Action? StateHasChanged { get; set; }

    Task<bool> LoginAsync(string username, string password);
    Task LogoutAsync();
}
