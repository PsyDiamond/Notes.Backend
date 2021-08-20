using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Notes.Application.Common.Mappings
{
    /// <summary>
    /// Создание автомаперов из сборки
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        /// <summary>
        /// Создание автомаперов
        /// </summary>
        /// <param name="assembly">сборка</param>
        public AssemblyMappingProfile(Assembly assembly) => ApplyMappingsFromAssebmly(assembly);

        /// <summary>
        /// Ядро автомапера
        /// </summary>
        /// <param name="assembly">сборка</param>
        private void ApplyMappingsFromAssebmly(Assembly assembly)
        {
            // Получение типов, которые реализуют IMapWith<>
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(x => x.IsGenericType 
                        && 
                        x.GetGenericTypeDefinition() == typeof(IMapWith<>)
                        )
                    ).ToList();

            // Создание всех этих типов и вызов у них метода Mapping
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new[] { this });
            }
        }
    }
}
