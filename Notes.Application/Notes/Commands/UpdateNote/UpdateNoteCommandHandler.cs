using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    /// <summary>
    /// Обработчик для команды - Обновление Заметки
    /// </summary>
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly INotesDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">контекст бд</param>
        public UpdateNoteCommandHandler(INotesDbContext dbContext)
            => _dbContext = dbContext;

        /// <summary>
        /// Обработать команду
        /// </summary>
        /// <param name="request">команда</param>
        /// <param name="cancellationToken">токет отмены</param>
        /// <returns>аналог void</returns>
        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            // Ищем заметку
            var entity = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            // Если не нашли - ругаемся
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            // Обновляем
            entity.Title = request.Title;
            entity.Details = request.Details;
            entity.EditDate = DateTime.Now;

            // Сохраняем в базе
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
