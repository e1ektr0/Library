using Library.Services;
using Library.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Admin;

[ApiController]
[Route("book")]
[Authorize(Roles = CustomRoles.Admin)]
public class AdminBookCategoryController: ControllerBase
{
    private readonly BookService _bookService;

    public AdminBookCategoryController(BookService bookService)
    {
        _bookService = bookService;
    }

    [Route("{id}/category/{categoryId}")]
    [HttpPost]
    public async Task AddCategory(long id, long categoryId)
    {
        await _bookService.AddCategory(id, categoryId);
    }
    
    [HttpDelete]
    [Route("{id}/category/{categoryId}")]
    public async Task RemoveCategory(long id, long categoryId)
    {
        await _bookService.RemoveCategory(id, categoryId);
    }
}