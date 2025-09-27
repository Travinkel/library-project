using LibraryProject.Api.DTOs;
using System;
using LibraryProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Services;

public class AuthorService
{
    private readonly LibraryDbContext _context;

    public AuthorService(LibraryDbContext context) => _context = context;

    public async Task<IReadOnlyList<AuthorDto>> GetAllAsync() =>
        await _context.Authors.AsNoTracking()
            .Select(a => new AuthorDto(a.Id, a.Name))
            .ToListAsync();

    public async Task<AuthorDto?> GetByIdAsync(string id)
    {
        var entity = await _context.Authors.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
        return entity is null ? null : new AuthorDto(entity.Id, entity.Name);
    }

    public async Task<string> CreateAsync(CreateAuthorDto dto)
    {
        var author = new Author
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return author.Id;
    }

    public async Task<AuthorDto?> UpdateAsync(string id, CreateAuthorDto dto)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return null;

        author.Name = dto.Name;
        await _context.SaveChangesAsync();

        return new AuthorDto(author.Id, author.Name);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

}

