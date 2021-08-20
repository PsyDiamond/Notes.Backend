using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// Представление о Заметке
    /// </summary>
    public class NoteLookupDto : IMapWith<Note>
    {
        /// <summary>
        /// Идентификатор заметки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок (название)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Выполнить маппинг
        /// </summary>
        /// <param name="profile">профиль автомаппера</param>
        public void Mapping(Profile profile)
        {
            // Маппинг одного класса на другой
            profile.CreateMap<Note, NoteLookupDto>()
                .ForMember(noteVm => noteVm.Title,
                    opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Id,
                    opt => opt.MapFrom(note => note.Id));
        }
    }
}
