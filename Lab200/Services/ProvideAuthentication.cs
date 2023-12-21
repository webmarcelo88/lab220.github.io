using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Lab200.Services;

public class ProvideAuthentication : AuthenticationStateProvider
{
    private const int SUBTRACT_TOKEN_EXPIRATION_IN_MINUTES = 3;
    private const string AUTHENTICATION_TYPE = "jwt";
    private const string CLAIM_TYPE_NAME = "unique_name";
    private const string CLAIM_TYPE_ROLE = "role";
    private const string CLAIM_TYPE_EMAIL = "email";
    private const string CLAIM_TYPE_CLIENT = "clientId";


    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly AuthenticationState _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    public ProvideAuthentication(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _protectedLocalStorage.GetAsync<string?>(Helpers.Data.TOKEN);
            var expires = await _protectedLocalStorage.GetAsync<DateTime?>(Helpers.Data.EXPIRES);

            if (string.IsNullOrWhiteSpace(token.Value) || expires.Value is null || HasTokenExpired(expires.Value))
            {
                return _anonymous;
            }
            await _protectedLocalStorage.SetAsync(Helpers.Data.EXPIRES, DateTime.UtcNow.AddMinutes(15));

            var state = BuildAuthenticationStateFromJwt(token.Value);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
        catch (Exception)
        {
            return _anonymous;
        }
    }
    private bool HasTokenExpired(DateTime? date)
    => date?.Subtract(TimeSpan.FromMinutes(SUBTRACT_TOKEN_EXPIRATION_IN_MINUTES)) < DateTime.UtcNow;

    private AuthenticationState BuildAuthenticationStateFromJwt(string token)
    {
        var claims = GetClaimsFromJwt(token);
        var convertedClaims = ClaimFromToClaimTypes(claims);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(convertedClaims, AUTHENTICATION_TYPE)));
    }
    private IEnumerable<Claim> GetClaimsFromJwt(string token)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(token);
        return jwtSecurityToken.Claims;
    }

    private static IEnumerable<Claim> ClaimFromToClaimTypes(IEnumerable<Claim> claims)
      => claims.Select(c =>
      {
          switch (c.Type)
          {
              case CLAIM_TYPE_CLIENT:
              case ClaimTypes.NameIdentifier:
                  return new Claim("ClientId", c.Value);

              case CLAIM_TYPE_NAME:
              case ClaimTypes.Name:
                  return new Claim(ClaimTypes.Name, c.Value);

              case CLAIM_TYPE_ROLE:
              case ClaimTypes.Role:
                  return new Claim(ClaimTypes.Role, c.Value);

              case CLAIM_TYPE_EMAIL:
              case ClaimTypes.Email:
                  return new Claim(ClaimTypes.Email, c.Value);

              default:
                  return c;
          }
      });
}
