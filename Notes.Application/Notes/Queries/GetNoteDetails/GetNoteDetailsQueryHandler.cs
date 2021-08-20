using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    /// <summary>
    /// Обработчик запроса списка заметок
    /// </summary>
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly INotesDbContext _dbContext;
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">контекст бд</param>
        /// <param name="mapper">автомаппер</param>
        public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper)
            => (_dbContext, _mapper) = (dbContext, mapper);

        /// <summary>
        /// Обработка запроса о деталях заметок
        /// </summary>
        /// <param name="request">запрос</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            // Поиск заметки
            var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

            // Если не нашли - ругаемся
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            // Автомаппинг Note на NoteDetailsVm
            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
