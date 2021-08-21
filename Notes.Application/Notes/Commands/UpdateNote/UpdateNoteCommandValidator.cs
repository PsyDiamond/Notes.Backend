using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    /// <summary>
    /// Валидатор обновления заметки
    /// </summary>
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UpdateNoteCommandValidator()
        {
            RuleFor(updateNoteCommand => updateNoteCommand.Title)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(updateNoteCommand => updateNoteCommand.UserId)
                .NotEqual(Guid.Empty);
            RuleFor(updateNoteCommand => updateNoteCommand.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
