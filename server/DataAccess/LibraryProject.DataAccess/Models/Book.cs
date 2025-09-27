using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryProject.DataAccess.Models;

public partial class Book
{
    public string Id { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Pages { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Genreid { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<AuthorBookJunction> AuthorBookJunctions { get; set; } = new List<AuthorBookJunction>();
}
