using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebAPI.Controllers
{
    /// <summary>
    /// КОнтроллер для Заметок
    /// </summary>
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        /// <summary>
        /// Автомаппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">автомаппер</param>
        public NoteController(IMapper mapper) 
            => _mapper = mapper;

        /// <summary>
        /// Получить список заметок
        /// </summary>
        /// <returns>вьюха с заметками</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            // Создание запроса
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };

            // Обработка запроса
            var vm = await Mediator.Send(query);

            // Результат
            return Ok(vm);
        }

        /// <summary>
        /// Получить информацию о конкретной заметке
        /// </summary>
        /// <param name="id">идентификатор заметки</param>
        /// <returns>вьюха с иноформацией о заметке</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            // Создание запроса
            var query = new GetNoteDetailsQuery
            {
                Id = id,
                UserId = UserId
            };

            // Обработка запроса
            var vm = await Mediator.Send(query);

            // Результат
            return Ok(vm);
        }

        /// <summary>
        /// Создать заметку
        /// </summary>
        /// <param name="createNoteDto">информация о заметке</param>
        /// <returns>идентификатор заметки</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            // Создание команды
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;

            // Обработка команды
            var noteId = await Mediator.Send(command);

            // Результат
            return Ok(noteId);
        }

        /// <summary>
        /// Обновить заметку
        /// </summary>
        /// <param name="updateNoteDto">информация о заметке</param>
        /// <returns>ничего</returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            // Создание команды
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;

            // Обработка команды
            await Mediator.Send(command);

            // Ничего
            return NoContent();
        }

        /// <summary>
        /// Удалить заметку
        /// </summary>
        /// <param name="id">идентификатор заметкм</param>
        /// <returns>ничего</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Создание команды
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId
            };

            // Обработка команды
            await Mediator.Send(command);

            // Ничего
            return NoContent();
        }
    }
}
