using FluentValidation;
using Microsoft.AspNetCore.Http;
using Notes.Application.Common.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notes.WebAPI.Middleware
{
    /// <summary>
    /// Компонент midleware - Обработчик исключений
    /// </summary>
    public class CustomExceptionHandlerMiddleware
    {
        /// <summary>
        /// Следующее действие
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="next">Следующее действие</param>
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
            => _next = next;

        /// <summary>
        /// Вызов обработчика
        /// </summary>
        /// <param name="context">контекст запроса</param>
        /// <returns>ничего</returns>
        public async Task Invoke(HttpContext context)
        {
            // Выполнение действие и ловля исключений
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обработка исключения
        /// </summary>
        /// <param name="context">контекст запроса</param>
        /// <param name="ex">исключение</param>
        /// <returns>ничего</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (ex)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
                result = JsonSerializer.Serialize(new 
                { 
                    error = ex.Message 
                });

            return context.Response.WriteAsync(result);
        }
    }
}
