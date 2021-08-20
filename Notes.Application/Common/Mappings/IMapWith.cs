using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    /// <summary>
    /// Интефейс маппинга
    /// </summary>
    /// <typeparam name="T">какую сущность надо смапить на другую</typeparam>
    public interface IMapWith<T>
    {
        /// <summary>
        /// Отобразить
        /// </summary>
        /// <param name="profile">профиль маппинга</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
