using System.Collections.Generic;
using LandmarkRemark.API.Models;
using LandmarkRemark.API.Services;
using LandmarkRemark.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [ValidateModel]
        public List<NoteDto> Index()
        {
            return _noteService.All();
        }

        [HttpPost("Create")]
        [ValidateModel]
        public NoteDto Create(CreateNoteRequest request)
        {
            return _noteService.Create(request);
        }

        [HttpPost("Delete")]
        public void Delete(DeleteNoteRequest request)
        {
            _noteService.Delete(request.NoteId);
        }

        [HttpGet("Search")]
        public List<NoteDto> Search(string term)
        {
            return _noteService.Search(term);
        }
    }
}
