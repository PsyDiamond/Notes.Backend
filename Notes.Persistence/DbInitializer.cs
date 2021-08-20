namespace Notes.Persistence
{
    /// <summary>
    /// Инициализатор базы данных
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// Произвести инициализацию
        /// </summary>
        /// <param name="context">контекст заметок</param>
        public static void Initializer(NotesDbContext context)
        {
            // Проверить, что база данных существует. И если нет - создаёт
            context.Database.EnsureCreated();
        }
    }
}
