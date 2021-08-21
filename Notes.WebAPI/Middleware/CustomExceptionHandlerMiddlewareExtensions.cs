using Microsoft.AspNetCore.Builder;

namespace Notes.WebAPI.Middleware
{
    /// <summary>
    /// Класс для методов расширения
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Включить обработку исключений
        /// </summary>
        /// <param name="builder">построитель приложения</param>
        /// <returns>построитель приложения</returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
