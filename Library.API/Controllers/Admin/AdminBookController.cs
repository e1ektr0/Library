using Library.API.Models;
using Library.Services;
using Library.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Admin;

[ApiController]
[Route("book")]
[Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.Admin)]
public class AdminBookController : ControllerBase
{
    private readonly BookService _bookService;

    public AdminBookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<Ok> Create([FromBody]BookCreateRequest model)
    {
        await _bookService.Create(model);
        return new Ok();
    }
    
    [HttpPut]
    public async Task<Ok> Update(BookUpdateRequest bookUpdateRequest)
    {
        await _bookService.Update(bookUpdateRequest);
        return new Ok();
    }
    
    [Route("{id}")]
    [HttpDelete]
    public async Task<Ok> Delete(long id)
    {
        await _bookService.Delete(id);
        return new Ok();
    }
}