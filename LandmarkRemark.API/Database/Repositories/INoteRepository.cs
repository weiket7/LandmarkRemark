using System.Collections.Generic;
using LandmarkRemark.API.Database.Entities;

namespace LandmarkRemark.API.Database.Repositories
{
    public interface INoteRepository
    {
        Note Create(Note note);
        List<Note> SearchByRemarkOrUsername(string term);
        List<Note> All();
        void Delete(int noteId);
    }
}