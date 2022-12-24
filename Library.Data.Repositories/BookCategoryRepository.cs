using Library.Data.Models;
using Library.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories;

public class BookCategoryRepository : BaseRepository
{
    public BookCategoryRepository(LibraryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<BookCategories?> Get(long bookId, long categoryId)
    {
        return await Context.BookCategories.FirstOrDefaultAsync(n => n.BookId == bookId && n.CategoryId == categoryId);
    }

    public async Task Add(BookCategories bookCategories)
    {
        await Context.BookCategories.AddAsync(bookCategories);
        await Context.SaveChangesAsync();
    }


    public void Remove(BookCategories category)
    {
        Context.BookCategories.Remove(category);
    }
}