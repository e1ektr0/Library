using System.Net;
using Library.API.Controllers.Admin.Models;
using Library.Data.Models;
using Library.Data.Repositories;
using Library.Exceptions;
using Mapster;

namespace Library.Services;

public class BookService
{
    private readonly BookRepository _bookRepository;
    private readonly BookCategoryRepository _bookCategoryRepository;
    private readonly BookFavoriteRepository _bookFavoriteRepository;

    public BookService(BookRepository bookRepository, BookCategoryRepository bookCategoryRepository,
        BookFavoriteRepository bookFavoriteRepository)
    {
        _bookRepository = bookRepository;
        _bookCategoryRepository = bookCategoryRepository;
        _bookFavoriteRepository = bookFavoriteRepository;
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

    public async Task AddCategory(long bookId, long categoryId)
    {
        await _bookCategoryRepository.Add(new BookCategory
        {
            BookId = bookId,
            CategoryId = categoryId
        });

        await _bookCategoryRepository.SaveAsync();
    }

    public async Task RemoveCategory(long id, long categoryId)
    {
        var bookCategories = await _bookCategoryRepository.Get(id, categoryId);
        if (bookCategories == null)
            throw new PortalException("Book Category not found", HttpStatusCode.NotFound);

        _bookCategoryRepository.Remove(bookCategories);
        await _bookCategoryRepository.SaveAsync();
    }

    public async Task Delete(long id)
    {
        var book = await _bookRepository.Get(id);
        if (book == null)
            throw new PortalException("Book not found", HttpStatusCode.NotFound);
        _bookRepository.Delete(book);
        await _bookRepository.SaveAsync();
    }

    public async Task AddToFavorites(long userId, long bookId)
    {
        await _bookFavoriteRepository.Add(new BookUserFavorite
        {
            BookId = bookId,
            UserId = userId
        });
        await _bookFavoriteRepository.SaveAsync();
    }
}