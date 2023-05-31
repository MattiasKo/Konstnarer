

using System.ComponentModel.DataAnnotations;

namespace Konstnarer.Models
{
    public class ValidateUser
    {
        [Key]
        public int Id { get; set; }

        public string RouteId { get; set; }
        public Guid UserId { get; set; }
    }
}
