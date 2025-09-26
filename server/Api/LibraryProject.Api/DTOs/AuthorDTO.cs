namespace LibraryProject.Api.DTOs;

public record AuthorDto(string Id, string Name);
public record CreateAuthorDto(string Name);