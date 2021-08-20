using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для связи обектов и сущностей базы данных
    /// </summary>
    public interface INotesDbContext
    {
        /// <summary>
        /// Коллекция заметок
        /// </summary>
        DbSet<Note> Notes { get; set; }

        /// <summary>
        /// Сохранить изменения асинхронно
        /// </summary>
        /// <param name="cancelationToken">токен отмены</param>
        /// <returns>количество сохраненных записей</returns>
        Task<int> SaveChangesAsync(CancellationToken cancelationToken);
    }
}
