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
    
    public async Task<List<BookDTO>> GetAllAsync() =>
        await _context.Books.AsNoTracking()
            .Select(b => new BookDTO(b.Id, b.Title, b.Pages, b.Genreid))
            .ToListAsync();
    
    public async Task<BookDTO?> GetByIdAsync(string id)
    {
        var b = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (b == null) return null;
        return new BookDTO(b.Id, b.Title, b.Pages, b.Genreid);
    }
    
    public async Task<BookDTO> CreateAsync(CreateBookDTO dto)
    {
        var book = new LibraryProject.DataAccess.Models.Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Pages = dto.Pages,
            Genreid = dto.GenreId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return new BookDTO(book.Id, book.Title, book.Pages, book.Genreid);
    }
    
    public async Task<BookDTO> UpdateAsync(string id, CreateBookDTO dto)
        => throw new NotImplementedException();

    public async Task DeleteAsync(string id)
        => throw new NotImplementedException();

}