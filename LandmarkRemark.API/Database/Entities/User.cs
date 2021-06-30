using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LandmarkRemark.API.Database.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        public List<Note> Notes { get; set; }
    }
}