using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using System;
using System.IO;
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
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:44385/";
                    options.Audience = "NotesWebAPI";
                    options.RequireHttpsMetadata = false;
                }
                );

            services.AddSwaggerGen(x =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
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
            
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty;
                x.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
            });

            // �������� ��������� ���������� 
            app.UseCustomExceptionHandler();
            
            // �������� EndpointRoutingMiddleware
            app.UseRouting();

            // ��������������� � HTTP �� HTTPS
            app.UseHttpsRedirection();

            // ���������� ���� ������� � �����
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            // ��������� ������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
