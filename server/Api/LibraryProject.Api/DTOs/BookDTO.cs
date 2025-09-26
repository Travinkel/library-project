namespace LibraryProject.Api.DTOs;

public record BookDTO(string Id, string Title, int Pages, string? GenreId);
public record CreateBookDTO(string Title, int Pages, string? GenreId);