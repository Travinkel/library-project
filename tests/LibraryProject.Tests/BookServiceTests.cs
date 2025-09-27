using System.Collections.Generic;
using System.IO;
using LibraryProject.Api;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using LibraryProject.DataAccess.Models;
using LibraryProject.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Npgsql;
using Testcontainers.PostgreSql;

public class BookServiceTests : ServiceTestBase<BookService>
{
    [Fact]
    public async Task CreateBook_WritesRow()
    {
        var dto = await Svc.CreateAsync(new CreateBookDTO("Harry Pothead and The Goblin On Fire", 250, null));
        var found = await Db.Books.AsNoTracking().SingleAsync(b => b.Id == dto.Id);

        Assert.Equal("Harry Pothead and The Goblin On Fire", found.Title);
        Assert.Equal(250, found.Pages);
        Assert.True(found.CreatedAt.HasValue);
    }
}