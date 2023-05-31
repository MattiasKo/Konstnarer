using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Konstnarer.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        public long PicSize { get; set; }
   
        [Display(Name ="Ladda up bild")]
        public byte[]? ImageFile { get; set; }
        public DateTime UploadDate { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Bildnamn")]
        public string PictureName { get; set; }
       
        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
        public bool AllowComments { get; set; }
        public List<PicComment>? PicComments { get; set; }
        [NotMapped]
        public Image? NewImage { get; set; }
        public Favorite? Favorites { get; set; }


    }
}
