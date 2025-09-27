using System;
using LibraryProject.Api.DTOs;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Services;

public class BookService
{
    private readonly LibraryDbContext _context;
    public BookService(LibraryDbContext context) => _context = context;
    
    public async Task<List<BookDto>> GetAllAsync() =>
        await _context.Books.AsNoTracking()
            .OrderBy(b => b.Title)
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
        var title = dto.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Book title must not be empty.", nameof(dto));

        if (dto.Pages <= 0)
            throw new ArgumentException("Pages must be greater than zero.", nameof(dto));

        // ✅ Valgfrit – tjek for eksisterende bogtitel
        var exists = await _context.Books.AnyAsync(b => b.Title == title);
        if (exists)
            throw new InvalidOperationException($"Book '{title}' already exists.");

        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Pages = dto.Pages,
            Genreid = string.IsNullOrWhiteSpace(dto.GenreId) ? null : dto.GenreId.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return new BookDto(book.Id, book.Title, book.Pages, book.Genreid);
    }

    
    public async Task<BookDto?> UpdateAsync(string id, CreateBookDto dto)
    {
        var title = dto.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Book title must not be empty.", nameof(dto));
        }

        if (dto.Pages <= 0)
        {
            throw new ArgumentException("Pages must be greater than zero.", nameof(dto));
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null) return null;

        book.Title = title;
        book.Pages = dto.Pages;
        book.Genreid = string.IsNullOrWhiteSpace(dto.GenreId) ? null : dto.GenreId?.Trim();
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
