using System.ComponentModel.DataAnnotations;

namespace LandmarkRemark.API.Models
{
    public class CreateNoteRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Remark is required")]
        public string Remark { get; set; }

        [Range(-90, 90)]
        [Required(ErrorMessage = "Latitude is required")]
        public decimal Latitude { get; set; }
        
        [Range(-180, 180)]
        [Required(ErrorMessage = "Latitude is required")]
        public decimal Longitude { get; set; }
    }
}