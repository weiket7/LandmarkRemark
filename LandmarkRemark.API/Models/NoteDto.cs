using LandmarkRemark.API.Database.Entities;

namespace LandmarkRemark.API.Models
{
    public class NoteDto
    {
        public NoteDto(Note note)
        {
            NoteId = note.NoteId;
            Remark = note.Remark;
            Latitude = note.Latitude;
            Longitude = note.Longitude;
            Username = note.User.Username;
        }

        public int NoteId { get; set; }
        public string Remark { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Username { get; set; }
    }
}
