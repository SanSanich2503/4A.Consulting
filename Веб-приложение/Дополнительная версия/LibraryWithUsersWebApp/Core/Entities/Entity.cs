namespace Core.Entities
{
    /// <summary>
    /// Абстрактный класс сущности
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание сущности
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}