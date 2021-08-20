using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Обработчик для команды Создать Заметку
    /// </summary>
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly INotesDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">контекст бд</param>
        public CreateNoteCommandHandler(INotesDbContext dbContext) 
            => _dbContext = dbContext;

        /// <summary>
        /// Обработать
        /// </summary>
        /// <param name="request">команда</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns>идентификатор заметки</returns>
        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            // Создание заметки
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now
            };

            // Добавление в репозиторий
            await _dbContext.Notes.AddAsync(note, cancellationToken);
            // Сохранение в базе
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Возврат результата
            return note.Id;
        }
    }
}
