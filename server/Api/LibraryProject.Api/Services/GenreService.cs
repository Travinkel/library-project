using LibraryProject.DataAccess;
using LibraryProject.Api.DTOs;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Services;

public class GenreService
{
    private readonly LibraryDbContext _context;
    public GenreService(LibraryDbContext context) => _context = context;

    public async Task<List<GenreDTO>> GetAllAsync() =>
        await _context.Genres
            .Select(g => new GenreDTO(g.Id, g.Name))
            .ToListAsync();

    public async Task<GenreDTO?> GetByIdAsync(string id)
    {
        var g = await _context.Genres.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (g == null) return null;
        return new GenreDTO(g.Id, g.Name);
    }

    public async Task<GenreDTO> CreateAsync(CreateGenreDTO dto)
    {
        var genre = new LibraryProject.DataAccess.Models.Genre
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow
        };
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return new GenreDTO(genre.Id, genre.Name);
    }
}