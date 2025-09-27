using System;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Tests;

public class AuthorServiceTests : ServiceTestBase<AuthorService>
{
    [Fact]
    public async Task CreateAuthor_WritesRow()
    {
        var id = await Svc.CreateAsync(new CreateAuthorDto("Ursula K. Le Guin"));
        var found = await Db.Authors.AsNoTracking().SingleAsync(a => a.Id == id);

        Assert.Equal("Ursula K. Le Guin", found.Name);
        Assert.True(found.CreatedAt.HasValue);
    }

    [Fact]
    public async Task CreateAuthor_EmptyName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(
            () => Svc.CreateAsync(new CreateAuthorDto(" ")));
        Assert.Contains("empty", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task UpdateAuthor_ChangesName()
    {
        var id = await Svc.CreateAsync(new CreateAuthorDto("Initial"));

        var updated = await Svc.UpdateAsync(id, new CreateAuthorDto("Opdateret"));

        Assert.NotNull(updated);
        Assert.Equal("Opdateret", updated!.Name);

        var entity = await Db.Authors.AsNoTracking().SingleAsync(a => a.Id == id);
        Assert.Equal("Opdateret", entity.Name);
    }
}
