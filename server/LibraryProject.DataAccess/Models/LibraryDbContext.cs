using Microsoft.EntityFrameworkCore;

namespace LibraryProject.DataAccess.Models;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<AuthorBookJunction> AuthorBookJunctions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("author_pkey");

            entity.ToTable("author", "library");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdat");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Books).WithMany(p => p.Authors)
                .UsingEntity<AuthorBookJunction>(
                    l => l.HasOne(ab => ab.Book)
                        .WithMany(b => b.AuthorBookJunctions)
                        .HasForeignKey(ab => ab.BookId)
                        .HasConstraintName("authorbookjunction_bookid_fkey"),
                    r => r.HasOne(ab => ab.Author)
                        .WithMany(a => a.AuthorBookJunctions)
                        .HasForeignKey(ab => ab.AuthorId)
                        .HasConstraintName("authorbookjunction_authorid_fkey"),
                    j =>
                    {
                        j.HasKey(t => new { t.AuthorId, t.BookId }).HasName("authorbookjunction_pkey");
                        j.ToTable("authorbookjunction", "library");
                        j.Property(t => t.AuthorId).HasColumnName("authorid");
                        j.Property(t => t.BookId).HasColumnName("bookid");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("book_pkey");

            entity.ToTable("book", "library");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdat");
            entity.Property(e => e.Genreid).HasColumnName("genreid");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.Genreid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("book_genreid_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genre_pkey");

            entity.ToTable("genre", "library");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdat");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<AuthorBookJunction>(entity =>
        {
            entity.HasKey(e => new { e.AuthorId, e.BookId }).HasName("authorbookjunction_pkey");

            entity.ToTable("authorbookjunction", "library");

            entity.Property(e => e.AuthorId).HasColumnName("authorid");
            entity.Property(e => e.BookId).HasColumnName("bookid");

            entity.HasOne(d => d.Author).WithMany(p => p.AuthorBookJunctions)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("authorbookjunction_authorid_fkey");

            entity.HasOne(d => d.Book).WithMany(p => p.AuthorBookJunctions)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("authorbookjunction_bookid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
