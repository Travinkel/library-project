using LibraryProject.DataAccess;
using LibraryProject.Api.DTOs;
using LibraryProject.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Services;

public class AuthorService
{
    private readonly LibraryDbContext _context;
    public AuthorService(LibraryDbContext context) => _context = context;

    public async Task<List<AuthorDTO>> GetAllAsync() =>
        await _context.Authors.AsNoTracking()
            .Select(a => new AuthorDTO(a.Id, a.Name))
            .ToListAsync();
    
    public async Task<AuthorDTO?> GetByIdAsync(string id)
    {
        var a = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (a == null) return null;
        return new AuthorDTO(a.Id, a.Name);
    }

    public async Task<AuthorDTO> CreateAsync(CreateAuthorDTO dto)
    {
        var author = new LibraryProject.DataAccess.Models.Author
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow
        };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return new AuthorDTO(author.Id, author.Name);
    }
    
    public async Task<AuthorDTO> UpdateAsync(string id, CreateAuthorDTO dto)
        => throw new NotImplementedException();

    public async Task DeleteAsync(string id)
        => throw new NotImplementedException();

}