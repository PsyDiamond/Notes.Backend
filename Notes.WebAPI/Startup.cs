using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebAPI.Middleware;
using System.Reflection;

namespace Notes.WebAPI
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Файл конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="configuration">конфигурация из файла</param>
        public Startup(IConfiguration configuration) 
            => Configuration = configuration;


        /// <summary>
        /// Конфигурация сервисов
        /// </summary>
        /// <param name="services">коллекция сервисов</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Автомаппер
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });

            // Медиатор
            services.AddApplication();

            // Хранение данных
            services.AddPersistence(Configuration);

            // Права доступа к сайту
            services.AddCors(options => {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            // Контроллеры
            services.AddControllers();
        }

        /// <summary>
        /// Конфигурация конвеера исполнения
        /// </summary>
        /// <param name="app">построитель приложений</param>
        /// <param name="env">окружение</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Если собрано в Debug - выводить исключения в явном виде
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Включает обработку исключений 
            app.UseCustomExceptionHandler();
            
            // Включает EndpointRoutingMiddleware
            app.UseRouting();

            // Перенаправление с HTTP на HTTPS
            app.UseHttpsRedirection();

            // Применение прав доступа к сайту
            app.UseCors("AllowAll");

            // Включение контроллеров
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
