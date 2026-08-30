using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Books
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(DataContext context) : base(context) { }

        public new IQueryable<Book> GetAll()
            => _context.Books
            .Include(x => x.Author)
            .Include(x => x.User)
            .AsQueryable()
            .OrderBy(x => x.User.Title).ThenBy(x => x.Author.Title).ThenBy(x => x.Title)
            .AsNoTracking();

        public IQueryable<Book> GetByUserId(int userId)
            => _context.Books
            .Where(x => x.UserId == userId)
            .Include(x => x.Author)
            .Include(x => x.User)
            .AsQueryable()
            .OrderBy(x => x.Author.Title).ThenBy(x => x.Title)
            .AsNoTracking();
    }
}