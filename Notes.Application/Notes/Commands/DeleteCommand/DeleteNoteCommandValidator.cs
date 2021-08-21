using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    /// <summary>
    /// Валидатор - удаления заметок
    /// </summary>
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DeleteNoteCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId)
                .NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
