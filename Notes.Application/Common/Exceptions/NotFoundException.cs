using System;

namespace Notes.Application.Common.Exceptions
{
    /// <summary>
    /// Исключение - Сущность не найдена
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">имя сущности</param>
        /// <param name="key">ключ</param>
        public NotFoundException(string name, object key) : 
            base($"Entity \"{name}\" ({key}) not found.") 
        { }
    }
}
