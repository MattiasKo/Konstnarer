using System.ComponentModel.DataAnnotations;

namespace Konstnarer.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}
