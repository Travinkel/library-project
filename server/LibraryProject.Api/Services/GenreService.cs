using System;
using LibraryProject.Api.DTOs;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Services;

public class GenreService
{
    private readonly LibraryDbContext _context;
    public GenreService(LibraryDbContext context) => _context = context;

    public async Task<List<GenreDto>> GetAllAsync() =>
        await _context.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new GenreDto(g.Id, g.Name))
            .ToListAsync();

    public async Task<GenreDto?> GetByIdAsync(string id)
    {
        var g = await _context.Genres.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (g == null) return null;
        return new GenreDto(g.Id, g.Name);
    }

    public async Task<GenreDto> CreateAsync(CreateGenreDto dto)
    {
        var name = dto.Name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Genre name must not be empty.", nameof(dto));

        var exists = await _context.Genres.AnyAsync(g => g.Name == name);
        if (exists)
            throw new InvalidOperationException($"Genre '{name}' already exists.");

        var genre = new Genre
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return new GenreDto(genre.Id, genre.Name);
    }

    
    public async Task<GenreDto?> UpdateAsync(string id, CreateGenreDto dto)
    {
        var name = dto.Name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Genre name must not be empty.", nameof(dto));
        }

        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return null;

        genre.Name = name;
        await _context.SaveChangesAsync();

        return new GenreDto(genre.Id, genre.Name);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }
}
