namespace Library.API.Controllers.Auth.Models;

public class LoginRequest
{
    public string Password { get; init; } = null!;
    public string UserName { get; init; } = null!;
}