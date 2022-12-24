using Library.Configs;
using Library.Data;
using Library.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.API;

public class DatabaseInitializer
{
    private readonly LibraryDbContext _context;
    private readonly GlobalConfig _globalConfig;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public DatabaseInitializer(LibraryDbContext context, GlobalConfig globalConfig, RoleManager<IdentityRole<long>> roleManager)
    {
        _context = context;
        _globalConfig = globalConfig;
        _roleManager = roleManager;
    }

    public async Task Init()
    {
        if (_globalConfig.Test)
            try
            {
                await _context.Database.EnsureDeletedAsync();
            }
            catch (Exception)
            {
                Thread.Sleep(2000);
                await _context.Database.EnsureDeletedAsync();
            }

        await _context.Database.MigrateAsync();


        await Seed();
    }

    private async Task Seed()
    {
        await _roleManager.CreateAsync(new IdentityRole<long>(CustomRoles.Admin));
    }
}