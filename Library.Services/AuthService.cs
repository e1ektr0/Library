using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Library.Configs;
using Library.Data.Models;
using Library.Exceptions;
using Library.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Library.Services;

public class AuthService 
{
    private readonly JwtConfig _jwtConfig;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthService(
        GlobalConfig config,
        SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        _jwtConfig = config.Jwt;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<TokensResponse> SignUp(string name, string password)
    {
        var user = new User
        {
            UserName = name,
        };
        var createUserResult = await _userManager.CreateAsync(user, password);
        createUserResult.ThrowException();
        var tokensResponse = await TokensProcess(user);
        return tokensResponse;
    }

    public async Task<LoginResponse> Login(string name, string password)
    {
        var user = await _userManager.FindByNameAsync(name);
        var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!checkPasswordResult.Succeeded)
            throw new PortalException(
                $"Incorrect email/password",
                HttpStatusCode.BadRequest
            );

        var tokensResponse = await TokensProcess(user);
        var result = new LoginResponse
        {
            AccessToken = tokensResponse.AccessToken,
            RefreshToken = tokensResponse.RefreshToken,
        };
        return result;
    }

    public async Task<TokensResponse> RefreshToken(string refreshToken)
    {
        var user = await _userManager.Users.Where(n => n.RefreshToken == refreshToken).FirstOrDefaultAsync();
        if (user == null || user.RefreshTokenExpire < DateTime.UtcNow)
            throw new PortalException(
                $"Token expired",
                HttpStatusCode.BadRequest
            );
        return await TokensProcess(user);
    }

    private string GetAccessToken(long userId, IReadOnlyCollection<string>? roles = null, bool withoutExpiration = false)
    {
        var now = DateTime.UtcNow;
        var claims = new List<Claim>
        {
            new(CustomClaims.UserId, userId.ToString()),
        };
        if (roles != null)
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwt = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            notBefore: now,
            claims: claims,
            expires: withoutExpiration ? DateTime.MaxValue : now.Add(TimeSpan.FromHours(_jwtConfig.ExpiresAccessTokenHours)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.SecretKey)),
                SecurityAlgorithms.HmacSha256));
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return token;
    }

    private static string GetRefreshToken()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }

    private TokensResponse GenerateTokens(long userId, IReadOnlyCollection<string>? roles = null)
    {
        var result = new TokensResponse
        {
            AccessToken = GetAccessToken(userId, roles),
            RefreshToken = GetRefreshToken()
        };
        return result;
    }

    private async Task<TokensResponse> TokensProcess(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var tokens = GenerateTokens(user.Id, roles.ToList());
        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpire = DateTime.UtcNow.AddDays(_jwtConfig.ExpiresRefreshTokenDays);

        var updateUserResult = await _userManager.UpdateAsync(user);
        updateUserResult.ThrowException();

        return new TokensResponse { AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken };
    }

}