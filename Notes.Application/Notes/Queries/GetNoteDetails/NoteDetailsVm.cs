using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    /// <summary>
    /// View Model Заметки
    /// </summary>
    public class NoteDetailsVm : IMapWith<Note>
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
        /// Детали (содержимое заметки)
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Дата созадания заметки
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата изменения заметки
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// Выполнить маппинг
        /// </summary>
        /// <param name="profile">профиль автомаппера</param>
        public void Mapping(Profile profile)
        {
            // Маппинг одного класса на другой
            profile.CreateMap<Note, NoteDetailsVm>()
                .ForMember(noteVm => noteVm.Title, 
                    opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Details, 
                    opt => opt.MapFrom(note => note.Details))
                .ForMember(noteVm => noteVm.Id,
                    opt => opt.MapFrom(note => note.Id))
                .ForMember(noteVm => noteVm.CreationDate,
                    opt => opt.MapFrom(note => note.CreationDate))
                .ForMember(noteVm => noteVm.EditDate,
                    opt => opt.MapFrom(note => note.EditDate));
        }
    }
}
