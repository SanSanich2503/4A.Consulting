using Core;
using Core.Entities.Authors;
using Core.Entities.Books;
using Core.Entities.Users;
using Data.ViewModels;
using Data.ViewModels.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class BookService : BaseService
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly User? _user;

        public BookService(DataContext context, IHttpContextAccessor contextAccessor, BookRepository bookRepository,
            AuthorRepository authorRepository, UserRepository userRepository) : base(context)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;

            var userGuid = contextAccessor.HttpContext?.User.Identity?.Name ?? "";
            _user = userRepository.GetCurrentUser(userGuid);
        }

        public BookForm BuildByForm(BookForm form)
            => new BookForm(form.Id, form.Title, form.Description, form.AuthorId, form.Year, form.Content, form.Price, GetAuthors());

        public BookForm BuildFormById(int id)
        {
            var book = _bookRepository.GetById(id).Result;
            if (book != null)
                return new BookForm(book.Id, book.Title, book.Description, book.AuthorId, book.Year, book.Content, book.Price, GetAuthors());

            return new BookForm();
        }

        public BookForm BuildForm() => new BookForm { Authors = GetAuthors() };

        public async Task<BookViewModelList> BuildViewModelList(int pageNumber, int pageSize, string title)
        {
            try
            {
                if (_user != null)
                {
                    var books = _user.Role.Title?.ToLower() == "админ"
                        ? _bookRepository.GetAll().AsAsyncEnumerable()
                        : _bookRepository.GetByUserId(_user.Id).AsAsyncEnumerable();
                    if (!string.IsNullOrWhiteSpace(title))
                        books = books
                            .Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title.ToLower()));

                    var booksList = await books.ToListAsync();
                    var count = booksList.Count;
                    var items = booksList.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .OrderBy(x => x.Title)
                        .Select(x => new BookViewModelItem
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Description = x.Description,
                            Author = x.Author.Title,
                            Year = x.Year,
                            Content = x.Content,
                            Price = x.Price,
                            User = x.User.Title
                        });

                    return new BookViewModelList
                    {
                        Items = items,
                        PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                        FilterViewModel = new FilterViewModel(title),
                        Count = count
                    };
                }

                return new BookViewModelList();
            }
            catch
            {
                return new BookViewModelList();
            }
        }

        public async Task<(bool, string)> Create(BookForm form)
        {
            try
            {
                if (_user != null)
                {
                    var book = new Book
                    {
                        Title = form.Title,
                        Description = form.Description,
                        AuthorId = form.AuthorId,
                        Year = form.Year,
                        Content = form.Content,
                        Price = form.Price,
                        UserId = _user.Id,
                        LastModified = DateTime.Now
                    };

                    await _bookRepository.Add(book);

                    return (true, "OK");
                }
            }
            catch
            {
                return (false, "Произошла внутренняя ошибка сервера");
            }

            return (false, "Элемент не найден");
        }

        public async Task<(bool, string)> Update(BookForm form)
        {
            try
            {
                if (_user != null)
                {
                    var book = _bookRepository.GetById(form.Id).Result;
                    if (book != null)
                    {
                        book.Title = form.Title;
                        book.Description = form.Description;
                        book.AuthorId = form.AuthorId;
                        book.Year = form.Year;
                        book.Content = form.Content;
                        book.Price = form.Price;
                        book.LastModified = DateTime.Now;

                        await _bookRepository.Update(book);

                        return (true, "OK");
                    }
                }
            }
            catch
            {
                return (false, "Произошла внутренняя ошибка сервера");
            }

            return (false, "Элемент не найден");
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var book = _bookRepository.GetById(id).Result;
                if (book != null)
                {
                    await _bookRepository.Remove(book);

                    return (true, "OK");
                }
            }
            catch
            {
                return (false, "Произошла внутренняя ошибка сервера");
            }

            return (false, "Элемент не найден");
        }

        private List<SelectListItem> GetAuthors()
        => _authorRepository.GetAll()
            .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
            .ToList();
    }
}