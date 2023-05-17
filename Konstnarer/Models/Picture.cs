using System.ComponentModel.DataAnnotations;

namespace Konstnarer.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public int PicSize { get; set; } = 0;
        public DateTime UploadDate { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string PictureName { get; set; }
        public int OwnerId { get; set; }
        public bool AllowComments { get; set; }
        public List<PicComment>? PicComments { get; set; }


    }
}
