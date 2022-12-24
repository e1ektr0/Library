using Library.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class LibraryDbContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    private readonly bool _initialized;

    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        _initialized = true;
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<BookCategory> BookCategories { get; set; } = null!;
    public DbSet<BookUserFavorite> BookUserFavorites { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookCategory>()
            .HasKey(c => new { c.BookId, c.CategoryId });
        modelBuilder.Entity<BookUserFavorite>()
            .HasKey(c => new { c.BookId, c.UserId });

        modelBuilder.Entity<Book>()
            .HasGeneratedTsVectorColumn(
                p => p.EngSearchVector,
                "english", // Text search config
                p => new { p.Name, p.Author }) // Included properties
            .HasIndex(p => p.EngSearchVector)
            .HasMethod("GIN");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IDateTimeEntity entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.UpdatedDate = now;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = now;
                        break;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!_initialized)
            options.UseNpgsql();
    }
}