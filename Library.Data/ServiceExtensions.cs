using Library.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Data;

public static class ServiceExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection collection, IConfiguration config)
    {
        collection.AddPooledDbContextFactory<LibraryDbContext>(o =>
        {
            var instance = new DatabaseConfig();
            var configurationSection = config.GetSection(DatabaseConfig.OptionsSection);
            configurationSection.Bind(instance);
            o.UseNpgsql(instance.ConnectionString!);
        });
        collection.AddDbContext<LibraryDbContext>(o =>
        {
            var instance = new DatabaseConfig();
            var configurationSection = config.GetSection(DatabaseConfig.OptionsSection);
            configurationSection.Bind(instance);
            o.UseNpgsql(instance.ConnectionString!);
        }, ServiceLifetime.Scoped, ServiceLifetime.Singleton);

        
        collection.AddScoped<DatabaseInitializer>();
        return collection;
    }
}