using System.Collections.Generic;
using System.IO;
using LibraryProject.Api;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using LibraryProject.DataAccess.Models;
using LibraryProject.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Npgsql;
using Testcontainers.PostgreSql;

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
}