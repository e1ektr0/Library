﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Data.Repositories;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection collection)
    {
        var repositories = typeof(ServiceExtensions).Assembly
            .GetTypes()
            .Where(n => n.Name.EndsWith("Repository"));
        foreach (var type in repositories)
            collection.AddScoped(type);

        return collection;
    }
}