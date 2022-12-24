using System.Text;
using Library.Configs;
using Library.Data;
using Library.Data.Models;
using Library.Data.Repositories;
using Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

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

        collection.AddControllers();
        collection.AddEndpointsApiExplorer();
        collection.AddSwaggerGen();

        collection.AddIdentity<User, IdentityRole<long>>().AddEntityFrameworkStores<LibraryDbContext>();
        collection.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        });
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection collection, IConfiguration config)
    {
        collection.AddAuthorization();
        collection
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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
        return collection;
    }
}