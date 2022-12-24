using Library.API.Controllers.Auth.Models;
using Library.Services;
using Library.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [Route("signup")]
    [HttpPost]
    public async Task<TokensResponse> SignUp(SignUpRequest model)
    {
        return await _authService.SignUp(model.UserName, model.Password);
    }

    [HttpPost]
    public async Task<LoginResponse> Login(LoginRequest model)
    {
        return await _authService.Login(model.UserName, model.Password);

    }
    
    [Route("refresh/{token}")]
    [HttpPost]
    public async Task<TokensResponse> Refresh(string token)
    {
        return await _authService.RefreshToken(token);
    }
}