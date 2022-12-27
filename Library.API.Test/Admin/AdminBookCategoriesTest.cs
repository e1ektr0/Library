using Library.API.Controllers.Admin;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Test.Admin;

public sealed class AdminBookCategoriesTest : BaseApiTest
{
    [Test]
    public async Task AddCategory()
    {
        await Base<AdminBookTest>().CreateBook();

        var book = await Context.Books.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        await Rait<AdminBookCategoryController>().Call(n => n.AddCategory(book.Id, category.Id));

        Assert.That(await Context.BookCategories.AnyAsync(), Is.True);
    }

    [Test]
    public async Task RemoveCategory()
    {
        await AddCategory();

        var book = await Context.Books.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        await Rait<AdminBookCategoryController>().Call(n => n.RemoveCategory(book.Id, category.Id));

        Assert.That(await Context.BookCategories.AnyAsync(), Is.False);
    }
}