using Library.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Library.API.Test;

public abstract class SetupApiTest
{
    protected IServiceProvider Services { get; set; } = null!;
    protected HttpClient Client { get; set; } = null!;
    protected LibraryDbContext Context { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        var app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(PrepareEnv);

        Client = app.CreateDefaultClient();
        Services = app.Services;
        Context = Services.GetService<LibraryDbContext>()!;
    }


    private void PrepareEnv(IWebHostBuilder _)
    {
        _.UseEnvironment("Test");
    }
}