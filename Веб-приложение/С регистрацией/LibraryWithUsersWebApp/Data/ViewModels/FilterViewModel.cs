namespace Data.ViewModels
{
    /// <summary>
    /// Класс для фильтрации
    /// </summary>
    public class FilterViewModel
    {
        public string Title { get; set; } = string.Empty;

        public FilterViewModel() { }

        public FilterViewModel(string title)
        {
            Title = title;
        }
    }
}