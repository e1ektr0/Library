namespace Library.API.Controllers.Auth.Models;

public class SignUpRequest
{
    public string Password { get; set; } = null!;
    public string UserName { get; set; } = null!;
}