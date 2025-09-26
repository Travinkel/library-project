using System;
using System.Collections.Generic;

namespace LibraryProject.DataAccess.Models;

public partial class Genre
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
