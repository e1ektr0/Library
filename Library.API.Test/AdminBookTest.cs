using Library.API.Controllers.Admin;
using Library.API.Controllers.Admin.Models;
using Library.API.Models;
using Microsoft.EntityFrameworkCore;
using RAIT.Core;

namespace Library.API.Test;

public class AdminBookTest : AuthTest
{
    [Test]
    public async Task CreateBook()
    {
        await LoginAdminTest();

        var model = new BookCreateRequest
        {
            Author = "Test",
            ImageUrl = "https://google.com",
            PagesCount = 100,
            PublishedDate = DateTime.UtcNow.AddDays(-1000)
        };
        
        await Client.Call<AdminBookController, Ok>(n => n.Create(model));


        var dbBook = await Context.Books.FirstAsync();
        Assert.That(dbBook.Author, Is.EqualTo(model.Author));
    }
}