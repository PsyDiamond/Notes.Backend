using System.Collections.Generic;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// View Model для заметок
    /// </summary>
    public class NoteListVm
    {
        // Коллекция заметок
        public IList<NoteLookupDto> Notes { get; set; }
    }
}
