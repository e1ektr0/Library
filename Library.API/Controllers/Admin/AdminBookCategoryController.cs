using Library.API.Models;
using Library.Services;
using Library.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Admin;

[ApiController]
[Route("book")]
[Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.Admin)]
public class AdminBookCategoryController : ControllerBase
{
    private readonly BookService _bookService;

    public AdminBookCategoryController(BookService bookService)
    {
        _bookService = bookService;
    }

    [Route("{bookId}/category/{categoryId}")]
    [HttpPost]
    public async Task<Ok> AddCategory(long bookId, long categoryId)
    {
        await _bookService.AddCategory(bookId, categoryId);
        return new Ok();
    }

    [HttpDelete]
    [Route("{bookId}/category/{categoryId}")]
    public async Task<Ok> RemoveCategory(long bookId, long categoryId)
    {
        await _bookService.RemoveCategory(bookId, categoryId);
        return new Ok();
    }
}