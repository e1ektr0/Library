using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        var services = typeof(ServiceExtensions).Assembly
            .GetTypes()
            .Where(n => n.Name.EndsWith("Service"));
        foreach (var type in services)
            collection.AddScoped(type);

        return collection;
    }
}