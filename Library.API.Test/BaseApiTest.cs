using Library.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RAIT.Core;

namespace Library.API.Test;

public abstract class BaseApiTest : BaseTest
{
    protected IServiceProvider Services => TestServerEndpoint.Services;
    protected HttpClient WebClient  => TestServerEndpoint.WebClient;
    protected LibraryDbContext Context  => TestServerEndpoint.Context;

    protected TB Base<TB>() where TB : BaseTest, new()
    {
        var baseTest = new TB
        {
            TestServerEndpoint = TestServerEndpoint
        };
        return baseTest;
    }

    protected RaitHttpWrapper<TController> Rait<TController>() where TController : ControllerBase
    {
        return new RaitHttpWrapper<TController>(WebClient);
    }

    [SetUp]
    public void Setup()
    {
        var app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(PrepareEnv);

        var client = app.CreateDefaultClient();
        var services = app.Services;
        var context = services.GetService<LibraryDbContext>()!;


        TestServerEndpoint = new TestServerEndpoint
        {
            Context = context,
            Services = services,
            WebClient = client
        };
    }


    private void PrepareEnv(IWebHostBuilder _)
    {
        _.UseEnvironment("Test");
    }
}