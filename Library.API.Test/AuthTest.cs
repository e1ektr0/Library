using Library.API.Controllers.Auth;
using Library.API.Controllers.Auth.Models;
using RAIT.Core;

namespace Library.API.Test;

public class AuthTest : SetupApiTest
{
    
    [Test]
    public async Task SignUpTest()
    {
        var model = new SignUpRequest
        {
            UserName = "maximfeofilov@gmail.com",
            Password = "BlaBlaBla"
        };
        var signUpRequestResult = await Сlient.Call<AuthController, SignUpRequestResult>(n => n.SignUp(model));
    }
}