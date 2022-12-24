using Library.Data.Models;

namespace Library.Data.Repositories;

public class BookRepository : BaseRepository
{
    public BookRepository(LibraryDbContext context) : base(context)
    {
    }

    public async Task Add(Book book)
    {
        await Context.Books.AddAsync(book);
    }
}