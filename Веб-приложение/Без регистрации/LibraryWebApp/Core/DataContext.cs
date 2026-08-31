using Core.Entities.Authors;
using Core.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core;

/// <summary>
/// Класс-контекст базы данных
/// </summary>
public class DataContext : DbContext
{
    public DbSet<Author> Authors { get; set; } // Таблицы авторов (в БД)
    public DbSet<Book> Books { get; set; } // Таблица книг (в БД)

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

    /// <summary>
    /// Получение строки подключения
    /// </summary>
    /// <returns></returns>
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