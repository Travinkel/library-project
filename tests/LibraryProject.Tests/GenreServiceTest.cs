using System;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Tests;

public class GenreServiceTests : ServiceTestBase<GenreService>
{
    [Fact]
    public async Task CreateGenre_WritesRow()
    {
        var dto = await Svc.CreateAsync(new CreateGenreDto("Magi"));
        var found = await Db.Genres.AsNoTracking().SingleAsync(g => g.Id == dto.Id);

        Assert.Equal("Magi", found.Name);
        Assert.True(found.CreatedAt.HasValue);
    }

    [Fact]
    public async Task CreateGenre_EmptyName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(
            () => Svc.CreateAsync(new CreateGenreDto("")));
        Assert.Contains("empty", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task UpdateGenre_ChangesName()
    {
        var dto = await Svc.CreateAsync(new CreateGenreDto("Old"));

        var updated = await Svc.UpdateAsync(dto.Id!, new CreateGenreDto("New"));

        Assert.NotNull(updated);
        Assert.Equal("New", updated!.Name);

        var entity = await Db.Genres.AsNoTracking().SingleAsync(g => g.Id == dto.Id);
        Assert.Equal("New", entity.Name);
    }
}
