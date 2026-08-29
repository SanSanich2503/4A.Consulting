using Core.Entities.Authors;
using Core.Entities.Books;
using Data.Models.AppSettings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core;

public class DataContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

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
        var projectName = "LibraryWebApp";
        var connectionString = "";

        var parentDirectory = Directory.GetCurrentDirectory();
        for(int i = 0; i < 5; i++)
        {
            if (parentDirectory.EndsWith(projectName)
                && Directory.GetDirectories(parentDirectory).Any(name => name.EndsWith(projectName))) break;

            parentDirectory = Directory.GetParent(parentDirectory)?.FullName ?? "";
        }

        var settingsFile = @$"{parentDirectory}\{projectName}\appsettings.json";

        if (File.Exists(settingsFile))
        {
            var json = File.ReadAllText(settingsFile);
            var model = JsonConvert.DeserializeObject<AppSettingsModel>(json);
            connectionString = model?.ConnectionStrings?.DefaultConnection ?? "";
        }

        return connectionString;
    }
}