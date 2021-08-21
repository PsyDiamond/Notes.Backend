using FluentValidation;
using System;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// Валидатор для запросов - списка заметок
    /// </summary>
    public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public GetNoteListQueryValidator()
        {
            RuleFor(note => note.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
