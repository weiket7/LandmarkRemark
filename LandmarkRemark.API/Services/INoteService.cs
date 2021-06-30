using System.Collections.Generic;
using LandmarkRemark.API.Models;

namespace LandmarkRemark.API.Services
{
    public interface INoteService
    {
        NoteDto Create(CreateNoteRequest request);
        List<NoteDto> Search(string term);
        List<NoteDto> All();
        void Delete(int noteId);
    }
}