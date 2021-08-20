using MediatR;
using System;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    /// <summary>
    /// Команда - Удалить заметку
    /// </summary>
    public class DeleteNoteCommand : IRequest
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор заметки
        /// </summary>
        public Guid Id { get; set; }
    }
}
