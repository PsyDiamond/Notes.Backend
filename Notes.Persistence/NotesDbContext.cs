using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfigurations;

namespace Notes.Persistence
{
    /// <summary>
    /// Контекст Заметок
    /// </summary>
    public sealed class NotesDbContext : DbContext, INotesDbContext
    {
        /// <summary>
        /// Заметки
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        /// <summary>
        /// Создание DbContext
        /// </summary>
        /// <param name="options">опции</param>
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        { }

        /// <summary>
        /// Действие при создании модели
        /// </summary>
        /// <param name="builder">построитель модели</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Применение конфигурации
            builder.ApplyConfiguration(new NoteConfiguration());
            // Дальнейшее создание модели
            base.OnModelCreating(builder);
        }
    }
}
