using Core;
using Microsoft.EntityFrameworkCore;

namespace Tests;

/// <summary>
/// Класс для тестирования приложения
/// </summary>
public class Tests
{
    private DataContext _context; // Контекст базы данных

    /// <summary>
    /// Метод для инициализации данных
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var connectionString = DataContext.GetConnectionString();
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(connectionString)
            .Options;

        _context = new DataContext(options);
    }

    /// <summary>
    /// Првоерка наличия авторов в соответствующей таблице базы данных
    /// </summary>
    [Test]
    public void HasAuthors()
    {
        Assert.AreEqual(_context.Authors.Any(), true);
    }

    /// <summary>
    /// Удаление объектов из памяти
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}