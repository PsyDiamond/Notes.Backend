using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;

namespace Notes.Persistence
{
    /// <summary>
    /// Класс для метода расширения
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавить функции хранения в DI
        /// </summary>
        /// <param name="services">коллекция сервисов</param>
        /// <param name="configuration">конфигурация</param>
        /// <returns>коллекция сервисов</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Строка подключения из файла
            var connectionString = configuration["DbConnection"];
            // Связать dbcontext с реальной базой
            services.AddDbContext<NotesDbContext>(options => options.UseSqlite(connectionString) );
            // Публикую dbcontext
            services.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>());

            // Передаю управление дальше
            return services;
        }
    }
}
