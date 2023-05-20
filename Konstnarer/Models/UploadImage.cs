using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Konstnarer.Models
{
    public class UploadImage : PageModel
    {

     
        public IFormFile FileUpload { get; set; }
        public DateTime UploadDate { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Behöver ett filnamn med minst 3 bokstäver och max 50")]
        [Display(Name = "Bildnamn")]
        public string PictureName { get; set; }
        public bool AllowComments { get; set; } = true;



    }
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }

}
