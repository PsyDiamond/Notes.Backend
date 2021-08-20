using MediatR;
using System;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// Запрос заметок пользователя
    /// </summary>
    public class GetNoteListQuery : IRequest<NoteListVm>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
