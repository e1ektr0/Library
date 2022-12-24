using Library.API.Controllers.Auth;
using Library.API.Controllers.Auth.Models;
using Library.Services;
using Library.Services.Models;
using RAIT.Core;

namespace Library.API.Test;

public class AuthTest : SetupApiTest
{
    private TokensResponse? _tokenResponse;

    [Test]
    public async Task SignUpTest()
    {
        var model = new SignUpRequest
        {
            UserName = "maximfeofilov@gmail.com",
            Password = "BlaBlaBla"
        };

        _tokenResponse = await Сlient.Call<AuthController, TokensResponse>(n => n.SignUp(model));

        Assert.That(_tokenResponse!.AccessToken, Is.Not.Empty);
        Assert.That(_tokenResponse.RefreshToken, Is.Not.Empty);
    }

    [Test]
    public async Task LoginTest()
    {
        await SignUpTest();
        var model = new LoginRequest
        {
            UserName = "maximfeofilov@gmail.com",
            Password = "BlaBlaBla"
        };
        var response = await Сlient.Call<AuthController, LoginResponse>(n => n.Login(model));

        Assert.That(response!.AccessToken, Is.Not.Empty);
        Assert.That(response.RefreshToken, Is.Not.Empty);
    }

    [Test]
    public async Task RefreshTokenTest()
    {
        await SignUpTest();

        var response = await Сlient.Call<AuthController, TokensResponse>
            (n => n.Refresh(_tokenResponse!.RefreshToken));

        Assert.That(response!.AccessToken, Is.Not.Empty);
        Assert.That(response.RefreshToken, Is.Not.Empty);
    }
}