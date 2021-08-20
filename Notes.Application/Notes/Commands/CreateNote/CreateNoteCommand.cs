using MediatR;
using System;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Команда - создание заметки
    /// </summary>
    public class CreateNoteCommand : IRequest<Guid>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Заголовок заметки
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Детали заметки
        /// </summary>
        public string Details { get; set; }
    }
}
