using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// Обработчик запроса на получение списка заметок пользователя
    /// </summary>
    public class GetNoteListQueryHandler :
        IRequestHandler<GetNoteListQuery, NoteListVm>
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
        public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
            => (_dbContext, _mapper) = (dbContext, mapper);

        /// <summary>
        /// Обработать запрос - список заметок пользователя
        /// </summary>
        /// <param name="request">запрос</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _dbContext.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new NoteListVm { Notes = notesQuery };
        }
    }
}
