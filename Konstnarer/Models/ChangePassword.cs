using System.ComponentModel.DataAnnotations;

namespace Konstnarer.Models
{
    public class ChangePassword
    {
        [Key]
        public int Id { get; set; }

        public string RouteId { get; set; }
        public Guid UserId { get; set; }
        [Required, StringLength(50, MinimumLength = 4, ErrorMessage = "Behöver ett lösenord minst 4 tecken")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Bekräfta lösenord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenordet och bekräfta lösenord passar inte.")]
        public string ConfirmPassword { get; set; }
    }
}
