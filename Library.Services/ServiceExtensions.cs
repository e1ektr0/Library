using Library.Data.Models;
using Library.Services.Image;
using Library.Services.Models;
using Mapster;
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
        MappingConfigs();
        collection.AddScoped<IImageStorage, FileImageStorage>();
        return collection;
    }

    private static void MappingConfigs()
    {
        TypeAdapterConfig<BookUpdateRequest, Book>
            .NewConfig()
            .IgnoreNullValues(true);
    }
}