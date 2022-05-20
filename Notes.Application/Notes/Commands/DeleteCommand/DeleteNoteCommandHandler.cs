using MediatR;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    /// <summary>
    /// Обработчик команды удаления заметки
    /// </summary>
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly INotesDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">контекст бд</param>
        public DeleteNoteCommandHandler(INotesDbContext dbContext)
            => _dbContext = dbContext;
        /// <summary>
        /// Обработка команды удаления
        /// </summary>
        /// <param name="request">команда</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns>аналог void</returns>
        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            // Ищем заметку
            var entity = await _dbContext.Notes.FindAsync(new object [] { request.Id }, cancellationToken);

            // Если не нашли - ругаемся
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            // Удаляем
            _dbContext.Notes.Remove(entity);
            // Сохраняем в базе
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
