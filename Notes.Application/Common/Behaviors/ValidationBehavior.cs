using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Common.Behaviors
{
    /// <summary>
    /// Поведение валидатора
    /// </summary>
    /// <typeparam name="TRequest">запрос</typeparam>
    /// <typeparam name="TResponse">ответ</typeparam>
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Коллекция валидаторов
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="validators">валидаторы</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="request">запрос</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <param name="next">следующий обработчик по цепочке</param>
        /// <returns></returns>
        public Task<TResponse> Handle(TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            // Контекст валидации
            var context = new ValidationContext<TRequest>(request);

            // Проверка через все валидаторы и сбор ошибок
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(fail => fail != null)
                .ToList();

            // Если ошибки есть - ругаемся
            if (failures.Count != 0)
                throw new ValidationException(failures);

            // Передаём управление дальше
            return next();
        }
    }
}
