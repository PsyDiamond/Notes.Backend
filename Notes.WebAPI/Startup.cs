using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebAPI.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
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
            services.AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddApiVersioning();
        }

        /// <summary>
        /// ������������ �������� ����������
        /// </summary>
        /// <param name="app">����������� ����������</param>
        /// <param name="env">���������</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // ���� ������� � Debug - �������� ���������� � ����� ����
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                foreach(var description in provider.ApiVersionDescriptions)
                {
                    x.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
                x.RoutePrefix = string.Empty;
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

            app.UseApiVersioning();

            // ��������� ������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
