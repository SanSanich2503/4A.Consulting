using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Books
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(DataContext context) : base(context) { }

        public new IQueryable<Book> GetAll()
            => _context.Books
            .Include(x => x.Author)
            .AsQueryable()
            .OrderBy(x => x.Author.Title).ThenBy(x => x.Title)
            .AsNoTracking();
    }
}