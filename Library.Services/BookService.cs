using Library.Data.Models;
using Library.Data.Repositories;

namespace Library.Services;

public class BookService
{
    private readonly BookRepository _bookRepository;

    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Create(Book book)
    {
        await _bookRepository.Add(book);
        await _bookRepository.SaveAsync();
    }

    public async Task AddCategory(long id, long categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveCategory(long id, long categoryId)
    {
        throw new NotImplementedException();
    }
}