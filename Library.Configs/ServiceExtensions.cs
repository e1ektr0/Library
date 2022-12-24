using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Library.Configs;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection collection, IConfiguration config)
    {
        collection.Configure<GlobalConfig>(config.Bind);
        collection.AddSingleton(n => n.GetService<IOptions<GlobalConfig>>()!.Value);
        return collection;
    }
}