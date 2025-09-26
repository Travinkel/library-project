namespace LibraryProject.Api.DTOs;

public record AuthorDTO(string Id, string Name);
public record CreateAuthorDTO(string Name);