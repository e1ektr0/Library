using Library.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RAIT.Core;

namespace Library.API.Test;

public abstract class SetupApiTest
{
    protected IServiceProvider Services { get; set; } = null!;
    protected HttpClient WebClient { get; set; } = null!;
    protected LibraryDbContext Context { get; set; } = null!;


    protected RaitHttpWrapper<TController> Rait<TController>() where TController : ControllerBase
    {
        return new RaitHttpWrapper<TController>(WebClient);
    }

    [SetUp]
    public void Setup()
    {
        var app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(PrepareEnv);

        WebClient = app.CreateDefaultClient();
        Services = app.Services;
        Context = Services.GetService<LibraryDbContext>()!;
    }


    private void PrepareEnv(IWebHostBuilder _)
    {
        _.UseEnvironment("Test");
    }
}