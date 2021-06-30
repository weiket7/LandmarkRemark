using System;
using System.ComponentModel.DataAnnotations;

namespace LandmarkRemark.API.Database.Entities
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public User User { get; set; }
        public DateTime PostedOn { get; set; }
    }
}