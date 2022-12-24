using Library.API.Controllers.User.Model;
using Library.API.Models;
using Library.Data.Repositories;
using Library.Services;
using Library.Services.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.User;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookRepository _bookRepository;
    private readonly BookService _bookService;

    public BookController(BookRepository bookRepository, BookService bookService)
    {
        _bookRepository = bookRepository;
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<List<BookDto>> Search([FromQuery] BookSearchRequest model)
    {
        var search = await _bookRepository.Search(model.Query, model.Categories);
        return search.Select(n => n.Adapt<BookDto>()).ToList();
    }
   
    [Route("{bookId}/my")]
    [HttpPost]
    public async Task<Ok> AddToFavorites(long bookId)
    {
        await _bookService.AddToFavorites(User.GetUserId(), bookId);
        return new Ok();
    }
}