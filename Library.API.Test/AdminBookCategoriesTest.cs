using Library.API.Controllers.Admin;
using Library.API.Models;
using Microsoft.EntityFrameworkCore;
using RAIT.Core;

namespace Library.API.Test;

public class AdminBookCategoriesTest : AdminBookTest
{
    [Test]
    public async Task AddCategory()
    {
        await CreateBook();

        var book = await Context.Books.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        await Client.Call<AdminBookCategoryController, Ok>(n => n.AddCategory(book.Id, category.Id));

        Assert.That(await Context.BookCategories.AnyAsync(), Is.True);
    }

    [Test]
    public async Task RemoveCategory()
    {
        await AddCategory();

        var book = await Context.Books.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        await Client.Call<AdminBookCategoryController, Ok>(n => n.RemoveCategory(book.Id, category.Id));

        Assert.That(await Context.BookCategories.AnyAsync(), Is.False);
    }

   
}