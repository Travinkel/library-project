namespace LibraryProject.DataAccess.Models;

public partial class AuthorBookJunction
{
    public string AuthorId { get; set; } = null!;

    public string BookId { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
