using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Tests;

public class GenreServiceTests : ServiceTestBase<GenreService>
{
    [Fact]
    public async Task CreateGenre_WritesRow()
    {
        var dto = await Svc.CreateAsync(new CreateGenreDTO("Magi"));
        var found = await Db.Genres.AsNoTracking().SingleAsync(g => g.Id == dto.Id);

        Assert.Equal("Magi", found.Name);
        Assert.True(found.CreatedAt.HasValue);
    }
}