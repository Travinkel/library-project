using LibraryProject.DataAccess;
using LibraryProject.Api.DTOs;
using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Services;

public class BookService
{
    private readonly LibraryDbContext _context;
    public BookService(LibraryDbContext context) => _context = context;
    
    public async Task<List<BookDto>> GetAllAsync() =>
        await _context.Books.AsNoTracking()
            .Select(b => new BookDto(b.Id, b.Title, b.Pages, b.Genreid))
            .ToListAsync();
    
    public async Task<BookDto?> GetByIdAsync(string id)
    {
        var b = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (b == null) return null;
        return new BookDto(b.Id, b.Title, b.Pages, b.Genreid);
    }
    
    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Pages = dto.Pages,
            Genreid = dto.GenreId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return new BookDto(book.Id, book.Title, book.Pages, book.Genreid);
    }
    
    public async Task<BookDto?> UpdateAsync(string id, CreateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return null;

        book.Title = dto.Title;
        book.Pages = dto.Pages;
        book.Genreid = dto.GenreId;
        await _context.SaveChangesAsync();

        return new BookDto(book.Id, book.Title, book.Pages, book.Genreid);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

}