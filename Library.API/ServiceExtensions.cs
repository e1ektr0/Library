using System.Text;
using Library.Configs;
using Library.Data;
using Library.Data.Models;
using Library.Data.Repositories;
using Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Library.API;

public static class ServiceExtensions
{
    public static void AddApi(this IServiceCollection collection, ConfigurationManager config)
    {
        collection
            .AddRepositories()
            .AddServices()
            .AddConfig(config)
            .AddAuthentication(config)
            .AddDb(config);

        collection.AddScoped<DatabaseInitializer>();
        collection.AddControllers();
        collection.AddEndpointsApiExplorer();
        AddSwagger(collection);

        collection.AddIdentity<User, IdentityRole<long>>().AddEntityFrameworkStores<LibraryDbContext>();
        collection.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        });
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ego.Api", Version = "v1" });
            var securitySchema = new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
            c.AddSecurityRequirement(securityRequirement);
        });
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection collection, IConfiguration config)
    {
        collection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = config.GetSection(JwtConfig.OptionsSection).Get<JwtConfig>()!;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        collection.AddAuthorization();
        return collection;
    }
}