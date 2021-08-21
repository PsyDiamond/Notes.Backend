using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Persistence;
using System;

namespace Notes.WebAPI
{
    /// <summary>
    /// ������� ���������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ����� �����
        /// </summary>
        /// <param name="args">��������� ���������� ������</param>
        public static void Main(string[] args)
        {
            // ������� ����
            var host = CreateHostBuilder(args).Build();
            // ���������� ���� ������
            AttachDataBase(host);
            // ��������� ���������
            host.Run();
        }

        /// <summary>
        /// ���������� ���� ������
        /// </summary>
        /// <param name="host">����</param>
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
        /// ���������������� ����
        /// </summary>
        /// <param name="args">��������� ���������� ������</param>
        /// <returns>����������� �����</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
