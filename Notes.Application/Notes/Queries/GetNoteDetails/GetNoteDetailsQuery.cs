using MediatR;
using System;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    /// <summary>
    /// Запрос - Детали заметок
    /// </summary>
    public class GetNoteDetailsQuery : IRequest<NoteDetailsVm>
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
