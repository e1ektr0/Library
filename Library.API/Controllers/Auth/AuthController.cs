using System.Net;
using Library.API.Controllers.Auth.Models;
using Library.Data.Models;
using Library.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AuthController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<SignUpRequestResult> SignUp(SignUpRequest model)
    {
        var identityResult = await _userManager.CreateAsync(new User
        {
            UserName = model.UserName
        }, model.Password);
        if (!identityResult.Succeeded)
            throw new PortalException(identityResult.Errors.First().Description, HttpStatusCode.Forbidden);
        
        return new SignUpRequestResult();
    }
}