using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Behaviors;
using System.Reflection;

namespace Notes.Application
{
    /// <summary>
    /// Класс для методов раширения
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        /// <param name="services">коллекция сервисов</param>
        /// <returns>коллекция сервисов</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Активировать все медиаторы из текущей сборки
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Активировать все валиадторы из текущей сборки
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // Добавить валидатор, как сервис
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Передать управление дальше
            return services;
        }
    }
}
