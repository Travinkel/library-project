namespace LibraryProject.Api.DTOs;

public record BookDto(string Id, string Title, int Pages, string? GenreId);
public record CreateBookDto(string Title, int Pages, string? GenreId);