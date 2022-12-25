using System.Net.Http.Headers;
using Library.API.Controllers.Auth;
using Library.API.Controllers.Auth.Models;
using Library.Data.Models;
using Library.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.API.Test;

public class AuthTest : SetupApiTest
{
    private TokensResponse? _tokenResponse;

    [Test]
    public async Task SignUp()
    {
        var model = new SignUpRequest
        {
            UserName = "maximfeofilov@gmail.com",
            Password = "BlaBlaBla"
        };

        _tokenResponse = await Rait<AuthController>().Call(n => n.SignUp(model));

        Assert.That(_tokenResponse!.AccessToken, Is.Not.Empty);
        Assert.That(_tokenResponse.RefreshToken, Is.Not.Empty);
    }

    [Test]
    public async Task LoginTest()
    {
        await SignUp();
        var model = new LoginRequest
        {
            UserName = "maximfeofilov@gmail.com",
            Password = "BlaBlaBla"
        };
        var response = await Rait<AuthController>().Call(n => n.Login(model));

        Assert.That(response!.AccessToken, Is.Not.Empty);
        Assert.That(response.RefreshToken, Is.Not.Empty);
    }


    [Test]
    public async Task LoginAdmin()
    {
        await SignUp();
        var userManager = Services.GetService<UserManager<User>>();
        var user = await Context.Users.FirstAsync();
        await userManager!.AddToRolesAsync(user, new[] { CustomRoles.Admin });

        var model = new LoginRequest
        {
            UserName = "maximfeofilov@gmail.com",
            Password = "BlaBlaBla"
        };
        var response = await Rait<AuthController>().Call(n => n.Login(model));

        WebClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", response!.AccessToken);
        Assert.That(response.AccessToken, Is.Not.Empty);
        Assert.That(response.RefreshToken, Is.Not.Empty);
    }

    [Test]
    public async Task RefreshToken()
    {
        await SignUp();

        var response = await Rait<AuthController>().Call(n => n.Refresh(_tokenResponse!.RefreshToken));

        Assert.That(response!.AccessToken, Is.Not.Empty);
        Assert.That(response.RefreshToken, Is.Not.Empty);
    }
}