using Library.Configs;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.API;

public class DatabaseInitializer
{
    private readonly LibraryDbContext _context;
    private readonly GlobalConfig _globalConfig;

    public DatabaseInitializer(LibraryDbContext context, GlobalConfig globalConfig)
    {
        _context = context;
        _globalConfig = globalConfig;
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
    }
}