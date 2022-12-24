using Library.API.Controllers.User;
using Library.API.Controllers.User.Model;
using Library.API.Models;
using Library.API.Test.Admin;
using Microsoft.EntityFrameworkCore;
using RAIT.Core;

namespace Library.API.Test.UserTests;

public class UserSearchTest : AdminBookCategoriesTest
{
    [Test]
    public async Task Search()
    {
        await AddCategory();
        var book = await Context.Books.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        var model = new BookSearchRequest
        {
            Query = book.Author,
            Categories = new List<long>
            {
                category.Id
            }
        };

        var books = await Client.Call<BookController, List<BookDto>>(n => n.Search(model));

        Assert.That(books!, Has.Count);
    }


    [Test]
    public async Task AddToFavorites()
    {
        await AddCategory();
        var book = await Context.Books.FirstAsync();

        await Client.Call<BookController, Ok>(n => n.AddToFavorites(book.Id));

        Assert.That(await Context.BookUserFavorites.AnyAsync(), Is.True);
    }
}