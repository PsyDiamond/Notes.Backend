using MediatR;
using System;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    /// <summary>
    /// Команда - Обновить заметку
    /// </summary>
    public class UpdateNoteCommand : IRequest
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
    }
}
