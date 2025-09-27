using System;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Tests;

public class BookServiceTests : ServiceTestBase<BookService>
{
    [Fact]
    public async Task CreateBook_WritesRow()
    {
        var dto = await Svc.CreateAsync(new CreateBookDto("Harry Pothead and The Goblin On Fire", 250, null));
        var found = await Db.Books.AsNoTracking().SingleAsync(b => b.Id == dto.Id);

        Assert.Equal("Harry Pothead and The Goblin On Fire", found.Title);
        Assert.Equal(250, found.Pages);
        Assert.True(found.CreatedAt.HasValue);
    }

    [Fact]
    public async Task CreateBook_InvalidPages_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(
            () => Svc.CreateAsync(new CreateBookDto("Tiny Book", 0, null)));
        Assert.Contains("greater than", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task UpdateBook_ChangesValues()
    {
        var created = await Svc.CreateAsync(new CreateBookDto("Working Title", 120, null));

        var updated = await Svc.UpdateAsync(created.Id!, new CreateBookDto("Better Title", 222, null));

        Assert.NotNull(updated);
        Assert.Equal("Better Title", updated!.Title);
        Assert.Equal(222, updated.Pages);

        var entity = await Db.Books.AsNoTracking().SingleAsync(b => b.Id == created.Id);
        Assert.Equal("Better Title", entity.Title);
        Assert.Equal(222, entity.Pages);
    }
}
