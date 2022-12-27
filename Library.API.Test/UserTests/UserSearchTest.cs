using Library.API.Controllers.User;
using Library.API.Controllers.User.Model;
using Library.API.Test.Admin;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Test.UserTests;

public sealed class UserSearchTest : BaseApiTest
{
    [Test]
    public async Task Search()
    {
        await Base<AdminBookCategoriesTest>().AddCategory();
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

        var books =await Rait<BookController>().Call(n => n.Search(model));

        Assert.That(books!, Has.Count);
    }


    [Test]
    public async Task AddToFavorites()
    {
        await  Base<AdminBookCategoriesTest>().AddCategory();
        var book = await Context.Books.FirstAsync();

        await Rait<BookController>().Call(n => n.AddToFavorites(book.Id));

        Assert.That(await Context.BookUserFavorites.AnyAsync(), Is.True);
    }
}