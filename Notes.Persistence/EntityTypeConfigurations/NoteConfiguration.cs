using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain;

namespace Notes.Persistence.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация сущности Заметки
    /// </summary>
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        /// <summary>
        /// Настраивает сущность
        /// </summary>
        /// <param name="builder">Построитель, который будет использоваться для настройки типа сущности</param>
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            // Первичный ключ
            builder.HasKey(x => x.Id);
            // Уникальный индекс по ключу
            builder.HasIndex(x => x.Id).IsUnique();
            // Ограничение на заголовок
            builder.Property(x => x.Title).HasMaxLength(250);
        }
    }
}
