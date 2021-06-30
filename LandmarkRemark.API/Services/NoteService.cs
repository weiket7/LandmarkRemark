using System.Collections.Generic;
using System.Linq;
using LandmarkRemark.API.Database.Entities;
using LandmarkRemark.API.Database.Repositories;
using LandmarkRemark.API.Models;
using LandmarkRemark.API.Utilities;

namespace LandmarkRemark.API.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IClock _clock;
        private readonly IUserService _userService;

        public NoteService(INoteRepository noteRepository, IClock clock, IUserService userService)
        {
            _noteRepository = noteRepository;
            _clock = clock;
            _userService = userService;
        }

        public NoteDto Create(CreateNoteRequest request)
        {
            User user = _userService.GetOrCreate(request.Username);
            
            Note note = new Note
            {
                UserId = user.UserId,
                User = null,
                Remark = request.Remark,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PostedOn = _clock.Now(),
            };
            Note newNote = _noteRepository.Create(note);
            newNote.User = user;
            return new NoteDto(newNote);
        }

        public List<NoteDto> Search(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return All();
            }
            var notes = _noteRepository.SearchByRemarkOrUsername(term);
            return notes.Select(x => new NoteDto(x)).ToList();
        }

        public List<NoteDto> All()
        {
            return _noteRepository.All().Select(x => new NoteDto(x)).ToList(); ;
        }

        public void Delete(int noteId)
        {
            _noteRepository.Delete(noteId);
        }
    }
}