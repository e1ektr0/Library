using Library.API.Controllers.Admin;
using Library.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Test.Admin;

public class AdminBookTest : AuthTest
{
    [Test]
    public async Task CreateBook()
    {
        await LoginAdmin();

        var model = new BookCreateRequest
        {
            Author = "Test",
            ImageUrl = "https://google.com",
            PagesCount = 100,
            PublishedDate = DateTime.UtcNow.AddDays(-1000),
            Name = "Name"
        };

        await Rait<AdminBookController>().Call(n => n.Create(model));


        var dbBook = await Context.Books.FirstAsync();
        Assert.That(dbBook.Author, Is.EqualTo(model.Author));
    }

    [Test]
    public async Task UpdateBook()
    {
        await CreateBook();
        var dbBook = await Context.Books.FirstAsync();
        var model = new BookUpdateRequest
        {
            BookId = dbBook.Id,
            Author = "Test2"
        };

        await Rait<AdminBookController>().Call(n => n.Update(model));

        await Context.Entry(dbBook).ReloadAsync();
        dbBook = await Context.Books.FirstAsync();

        Assert.That(dbBook.Author, Is.EqualTo(model.Author));
        Assert.That(dbBook.ImageUrl, Is.Not.Null);
    }

    [Test]
    public async Task DeleteBook()
    {
        await CreateBook();
        var dbBook = await Context.Books.FirstAsync();
      
        await Rait<AdminBookController>().Call(n => n.Delete(dbBook.Id));
      
        dbBook = await Context.Books.FirstOrDefaultAsync();

        Assert.That(dbBook, Is.Null);
    }
}