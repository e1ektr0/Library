using System.Net;
using Library.API.Controllers.Admin.Models;
using Library.Data.Repositories;
using Library.Exceptions;
using Mapster;

namespace Library.Services;

public class BookService
{
    private readonly BookRepository _bookRepository;

    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Create(BookCreateRequest book)
    {
        await _bookRepository.Add(book.ToDb());
        await _bookRepository.SaveAsync();
    }

    public async Task Update(BookUpdateRequest bookUpdateRequest)
    {
        var book = await _bookRepository.Get(bookUpdateRequest.BookId);
        if (book == null)
            throw new PortalException("Book not found", HttpStatusCode.NotFound);
        bookUpdateRequest.Adapt(book);

        await _bookRepository.SaveAsync();
    }

    public async Task AddCategory(long id, long categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveCategory(long id, long categoryId)
    {
    }

    public async Task Delete(long id)
    {
        var book = await _bookRepository.Get(id);
        if (book == null)
            throw new PortalException("Book not found", HttpStatusCode.NotFound);
        _bookRepository.Delete(book);
        await _bookRepository.SaveAsync();
    }
}