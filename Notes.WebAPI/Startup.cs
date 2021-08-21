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
    /// ������������ ����������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���� ������������
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="configuration">������������ �� �����</param>
        public Startup(IConfiguration configuration) 
            => Configuration = configuration;


        /// <summary>
        /// ������������ ��������
        /// </summary>
        /// <param name="services">��������� ��������</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // ����������
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });

            // ��������
            services.AddApplication();

            // �������� ������
            services.AddPersistence(Configuration);

            // ����� ������� � �����
            services.AddCors(options => {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            // �����������
            services.AddControllers();
        }

        /// <summary>
        /// ������������ �������� ����������
        /// </summary>
        /// <param name="app">����������� ����������</param>
        /// <param name="env">���������</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ���� ������� � Debug - �������� ���������� � ����� ����
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // �������� ��������� ���������� 
            app.UseCustomExceptionHandler();
            
            // �������� EndpointRoutingMiddleware
            app.UseRouting();

            // ��������������� � HTTP �� HTTPS
            app.UseHttpsRedirection();

            // ���������� ���� ������� � �����
            app.UseCors("AllowAll");

            // ��������� ������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
