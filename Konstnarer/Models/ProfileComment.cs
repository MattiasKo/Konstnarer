using System.ComponentModel.DataAnnotations;

namespace Konstnarer.Models
{

        public class ProfileComment
        {
            [Key]
            public int Id { get; set; }
            public Guid PersonId { get; set; }
            public Guid UserId { get; set; }//den som ger commentaren
            [StringLength(250)]
            public string Comment { get; set; }

        }
    
}
