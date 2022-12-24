using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Library.API.Test;

public abstract class SetupApiTest
{
    private WebApplicationFactory<Program> _application;
    protected IServiceProvider Services { get; set; } = null!;
    protected HttpClient Сlient { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(PrepareEnv);

        Сlient = _application.CreateDefaultClient();
        Services = _application.Services;
    }

    private void PrepareEnv(IWebHostBuilder _)
    {
        _.UseEnvironment("Test");
    }
}