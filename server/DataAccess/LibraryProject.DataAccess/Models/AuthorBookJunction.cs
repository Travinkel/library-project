using System.ComponentModel.DataAnnotations;

namespace LibraryProject.DataAccess.Models;

public partial class AuthorBookJunction
{
    [Required(AllowEmptyStrings = false)]
    public string AuthorId { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string BookId { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
