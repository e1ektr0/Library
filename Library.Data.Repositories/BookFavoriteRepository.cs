using Library.Data.Models;

namespace Library.Data.Repositories;

public class BookFavoriteRepository : BaseRepository
{
    public BookFavoriteRepository(LibraryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Add(BookUserFavorite bookUserFavorite)
    {
        await Context.BookUserFavorites.AddAsync(bookUserFavorite);
    }
}