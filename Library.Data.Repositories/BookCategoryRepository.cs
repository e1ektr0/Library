using Library.Data.Models;
using Library.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories;

public class BookCategoryRepository : BaseRepository
{
    public BookCategoryRepository(LibraryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<BookCategory?> Get(long bookId, long categoryId)
    {
        return await Context.BookCategories.FirstOrDefaultAsync(n => n.BookId == bookId && n.CategoryId == categoryId);
    }

    public async Task Add(BookCategory bookCategory)
    {
        await Context.BookCategories.AddAsync(bookCategory);
        await Context.SaveChangesAsync();
    }


    public void Remove(BookCategory category)
    {
        Context.BookCategories.Remove(category);
    }
}