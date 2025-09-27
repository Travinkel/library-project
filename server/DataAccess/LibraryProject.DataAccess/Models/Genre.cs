using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryProject.DataAccess.Models;

public partial class Genre
{
    public string Id { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
