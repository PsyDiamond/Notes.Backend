using System;

namespace Notes.Domain
{
    /// <summary>
    /// Заметка
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор заметки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок (название)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Детали (содержимое заметки)
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Дата созадания заметки
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата изменения заметки
        /// </summary>
        public DateTime? EditDate { get; set; }
    }
}
