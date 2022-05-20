using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
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
        /// <remarks>Пример использования
        /// GET /note
        /// </remarks>
        /// <returns>вьюха с заметками</returns>
        /// <response code="200">Успешно</response>
        /// <responce code="401">Нет доступа</responce>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>Пример использования:
        /// GET /note/0E69F2E6-FAFE-454C-AB73-5A879B6B8BDB
        /// </remarks>
        /// <returns>вьюха с иноформацией о заметке</returns>
        /// <response code="200">Успешно</response>
        /// <responce code="401">Нет доступа</responce>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>Пример использования
        /// POST /note
        /// {
        ///     title: "note title",
        ///     details: "note details"
        /// }     
        /// </remarks>
        /// <returns>идентификатор заметки</returns>
        /// <response code="201">Успешно</response>
        /// <responce code="401">Нет доступа</responce>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>Пример использования
        /// PUT /note
        /// {
        ///     title: "updated title"
        /// }
        /// </remarks>
        /// <returns>ничего</returns>
        /// <response code="204">Успешно</response>
        /// <responce code="401">Нет доступа</responce>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>пример использования
        /// DELETE /note/0E69F2E6-FAFE-454C-AB73-5A879B6B8BDB
        /// </remarks>
        /// <returns>ничего</returns>
        /// <response code="204">Успешно</response>
        /// <responce code="401">Нет доступа</responce>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
