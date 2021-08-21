using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.UpdateNote;
using System;

namespace Notes.WebAPI.Models
{
    /// <summary>
    /// Представление - Обновить заметку
    /// </summary>
    public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
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
        /// Выполнить маппинг UpdateNoteDto на UpdateNoteCommand
        /// </summary>
        /// <param name="profile">профиль автомаппера</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Id, opt => opt.MapFrom(noteDto => noteDto.Id))
                .ForMember(noteCommand => noteCommand.Title, opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(noteCommand => noteCommand.Details, opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}
