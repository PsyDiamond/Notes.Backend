using FluentValidation;
using System;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    /// <summary>
    /// Валидатор - запрос деталей заметки
    /// </summary>
    public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public GetNoteDetailsQueryValidator()
        {
            RuleFor(note => note.UserId)
                .NotEqual(Guid.Empty);
            RuleFor(note => note.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
