namespace Core.Entities.Authors
{
    public class AuthorRepository : BaseRepository<Author>
    {
        public AuthorRepository(DataContext context) : base(context) { }
    }
}