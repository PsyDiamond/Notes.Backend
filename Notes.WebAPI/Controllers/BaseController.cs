using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Notes.WebAPI.Controllers
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// В данно реализации - MediatR
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Медиатор - Для обработки команд и запросов
        /// </summary>
        protected IMediator Mediator => 
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        internal Guid UserId =>
            !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
