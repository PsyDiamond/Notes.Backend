using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNote;
using System.ComponentModel.DataAnnotations;

namespace Notes.WebAPI.Models
{
    /// <summary>
    /// Представление для создания Заметки
    /// </summary>
    public class CreateNoteDto : IMapWith<CreateNoteCommand>
    {
        /// <summary>
        /// Заголовок (название)
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Детали (содержимое заметки)
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Выполнить маппинг CreateNoteDto на CreateNoteCommand
        /// </summary>
        /// <param name="profile">профиль для автомаппинга</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Title, opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(noteCommand => noteCommand.Details, opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}
