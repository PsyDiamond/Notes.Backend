using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Persistence;
using System;

namespace Notes.WebAPI
{
    /// <summary>
    /// Главная программа
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args">аргументы коммандной строки</param>
        public static void Main(string[] args)
        {
            // Создать Хост
            var host = CreateHostBuilder(args).Build();
            // Подключить базу данных
            AttachDataBase(host);
            // Запустить программу
            host.Run();
        }

        /// <summary>
        /// Подключить базу данных
        /// </summary>
        /// <param name="host">хост</param>
        private static void AttachDataBase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initializer(context);
                }
                catch (Exception ex)
                {
                }
            }
        }
        /// <summary>
        /// Сконфигурировать хост
        /// </summary>
        /// <param name="args">аргументы коммандной строки</param>
        /// <returns>построитель хоста</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
