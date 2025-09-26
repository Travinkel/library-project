using LibraryProject.Api.DTOs;

namespace LibraryProject.Api.Services;

public interface IAuthorService
{
    Task<IReadOnlyList<AuthorDto>> GetAllAsync();
    Task<AuthorDto?> GetByIdAsync(string id);
    Task<string> CreateAsync(CreateAuthorDto dto);
    Task<AuthorDto> UpdateAsync(string id, CreateAuthorDto dto);
    Task DeleteAsync(string id);
}
