using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Notes.Application
{
    /// <summary>
    /// Класс для методов раширения
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавить поддержку паттерна Медиатор
        /// </summary>
        /// <param name="services">коллекция сервисов</param>
        /// <returns>коллекция сервисов</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Активировать все медиаторы из текущей сборки
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Передать управление дальше
            return services;
        }
    }
}
