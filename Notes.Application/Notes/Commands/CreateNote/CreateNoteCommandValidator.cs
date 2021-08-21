using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Валидатор создания заметки
    /// </summary>
    public class CreateNoteCommandValidator :  AbstractValidator<CreateNoteCommand>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CreateNoteCommandValidator()
        {
            RuleFor(createNoteCommand => createNoteCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createNoteCommand => createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
