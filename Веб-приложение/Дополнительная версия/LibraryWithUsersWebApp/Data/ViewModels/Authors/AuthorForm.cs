namespace Data.ViewModels.Authors
{
    public class AuthorForm : Form
    {
        public AuthorForm() { }

        public AuthorForm(int id, string? title, string? description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}