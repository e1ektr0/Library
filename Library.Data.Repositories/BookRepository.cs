using Library.Data.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Book?> Get(long bookId)
    {
        return await Context.Books.FirstOrDefaultAsync(n => n.Id == bookId);
    }

    public void Delete(Book book)
    {
        Context.Books.Remove(book);
    }
}