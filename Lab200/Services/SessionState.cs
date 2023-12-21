using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab200.Helpers;
using Microsoft.AspNetCore.Components;
using Lab200.Data;

namespace Lab200.Services;

public class SessionState : ISessionState
{
    private const string KEY = "lab220_key_95768741258jWt";
    private const string ACCESS_TOKEN_TYPE = "at+jwt";

    private readonly ProtectedLocalStorage protectedLocalStorage;
    private readonly NavigationManager _navigationManager;
    private readonly IUserService _userService;

    public SessionState(IUserService userService, ProtectedLocalStorage protectedLocalStorage, NavigationManager navigationManager)
    {
        _userService = userService;
        this.protectedLocalStorage = protectedLocalStorage;
        _navigationManager = navigationManager;
    }

    public User? User {get; protected set;}

    public Timer Timer { get; protected set; }
    public Action? StateHasChanged { get; set; }

    public async Task<bool> LoginAsync(string username, string password)
    {
        User = await _userService.LoginAsync(username, password);

        if(User == null)
            return false;

        User.Password = string.Empty;

        var token = GenerateTokenAsync();
        await InsertTokenIntoLocalStorage(token);

        return true;
    }

    public string GenerateTokenAsync()
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(KEY);
        var claims = GetClaims();
        var identityClaims = new ClaimsIdentity();

        identityClaims.AddClaims(claims);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            NotBefore = DateTime.UtcNow,
            IssuedAt = DateTime.UtcNow,
            TokenType = ACCESS_TOKEN_TYPE,
            Subject = identityClaims
        };

        var securityToken = handler.CreateToken(securityTokenDescriptor);
        var encodedJwt = handler.WriteToken(securityToken);

        return encodedJwt;
    }

    private IList<Claim> GetClaims()
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, User!.Name),
            new Claim(ClaimTypes.Email, User!.Email),
            new Claim(ClaimTypes.Role, User!.Role!),
        };

        if (User.ClientId != null)
            claims.Add(new("ClientId", User!.ClientId!.ToString()));

        return claims;
    }

    private async Task InsertTokenIntoLocalStorage(string? token = null)
    {
        await protectedLocalStorage.SetAsync(Helpers.Data.TOKEN, token);
        await protectedLocalStorage.SetAsync(Helpers.Data.EXPIRES, DateTime.UtcNow.AddMinutes(15));
    }

    public async Task LogoutAsync()
    {
        await protectedLocalStorage.DeleteAsync(Helpers.Data.TOKEN);
        await protectedLocalStorage.DeleteAsync(Helpers.Data.EXPIRES);

        _navigationManager.NavigateTo("/Login");
    }
}
