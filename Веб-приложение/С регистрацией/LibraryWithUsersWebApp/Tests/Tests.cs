using Core;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class Tests
{
    private DataContext _context;

    [SetUp]
    public void Setup()
    {
        var connectionString = DataContext.GetConnectionString();
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(connectionString)
            .Options;

        _context = new DataContext(options);
    }

    [Test]
    public void HasAuthors()
    {
        Assert.AreEqual(_context.Authors.Any(), true);
    }

    [Test]
    public void HasUsers()
    {
        Assert.AreEqual(_context.Users.Any(), true);
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}