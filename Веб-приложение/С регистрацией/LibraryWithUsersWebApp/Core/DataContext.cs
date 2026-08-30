using Core.Entities.Authors;
using Core.Entities.Books;
using Core.Entities.Roles;
using Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core;

public class DataContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Content)
                .HasColumnType("xml"); // Принудительно задаем тип xml в Postgres
        });
    }

    public static string GetConnectionString()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var config = builder.Build();

        return config.GetConnectionString("DefaultConnection") ?? "";
    }
}