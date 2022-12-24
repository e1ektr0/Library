using Library.API.Controllers.Admin.Models;
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
        await _bookService.Create(model.ToDb());
        return new Ok();
    }
    
    [HttpPut]
    public async Task Update()
    {
        
    }
    
    [HttpDelete]
    public async Task Delete()
    {
        
    }
    
    
}