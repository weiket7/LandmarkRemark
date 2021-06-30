using System.Collections.Generic;
using System.Linq;
using LandmarkRemark.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandmarkRemark.API.Database.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DatabaseContext _databaseContext;

        public NoteRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Note Create(Note note)
        {
            note.User = null;
            _databaseContext.Add(note);
            _databaseContext.SaveChanges();
            return note;
        }

        public List<Note> SearchByRemarkOrUsername(string term)
        {
            term = term.Trim().ToLower();
            return _databaseContext.Notes.Include(c => c.User)
                .Where(x => x.Remark.ToLower().Contains(term) || x.User.Username.ToLower().Contains(term)).ToList();
        }

        public List<Note> All()
        {
            return _databaseContext.Notes.Include(c => c.User).ToList();
        }

        public void Delete(int noteId)
        {
            Note note = _databaseContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
            if (note != null)
            {
                _databaseContext.Remove(note);
                _databaseContext.SaveChanges();
            }
        }
    }

}
