using Core;
using Core.Entities.Authors;
using Data.ViewModels;
using Data.ViewModels.Authors;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    /// <summary>
    /// Сервис для работы с авторами
    /// </summary>
    public class AuthorService : BaseService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(DataContext context, AuthorRepository authorRepository) : base(context)
        {
            _authorRepository = authorRepository;
        }

        public AuthorForm BuildByForm(AuthorForm form) => new AuthorForm(form.Id, form.Title, form.Description);

        public AuthorForm BuildFormById(int id)
        {
            var author = _authorRepository.GetById(id).Result;
            if (author != null) return new AuthorForm(author.Id, author.Title, author.Description);

            return new AuthorForm();
        }

        public AuthorForm BuildForm() => new AuthorForm();

        public async Task<AuthorViewModelList> BuildViewModelList(int pageNumber, int pageSize, string title)
        {
            try
            {
                var authors = _authorRepository.GetAll().AsAsyncEnumerable();
                if (!string.IsNullOrWhiteSpace(title))
                    authors = authors
                        .Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title.ToLower()));

                var authorsList = await authors.ToListAsync();
                var count = authorsList.Count;
                var items = authorsList.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .OrderBy(x => x.Title)
                    .Select(x => new AuthorViewModelItem
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description
                    });

                return new AuthorViewModelList
                {
                    Items = items,
                    PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                    FilterViewModel = new FilterViewModel(title),
                    Count = count
                };
            }
            catch
            {
                return new AuthorViewModelList();
            }
        }

        public async Task<(bool, string)> Create(AuthorForm form)
        {
            try
            {
                var author = new Author
                {
                    Title = form.Title,
                    Description = form.Description,
                    LastModified = DateTime.Now
                };

                await _authorRepository.Add(author);

                return (true, "OK");
            }
            catch
            {
                return (false, "Произошла внутренняя ошибка сервера");
            }
        }

        public async Task<(bool, string)> Update(AuthorForm form)
        {
            try
            {
                var author = _authorRepository.GetById(form.Id).Result;
                if (author != null)
                {
                    author.Title = form.Title;
                    author.Description = form.Description;
                    author.LastModified = DateTime.Now;

                    await _authorRepository.Update(author);

                    return (true, "OK");
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
                var author = _authorRepository.GetById(id).Result;
                if (author != null)
                {
                    await _authorRepository.Remove(author);

                    return (true, "OK");
                }
            }
            catch
            {
                return (false, "Произошла внутренняя ошибка сервера");
            }

            return (false, "Элемент не найден");
        }
    }
}