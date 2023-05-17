using System.ComponentModel.DataAnnotations;

namespace Konstnarer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25, MinimumLength = 4, ErrorMessage = "Behöver minst 4 tecken max 50")]
        [DataType(DataType.Text)]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required (ErrorMessage = "Behöver ett giltig epost")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Epost")]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 4, ErrorMessage = "Behöver ett lösenord minst 4 tecken")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
        [StringLength(25)]
        [DataType(DataType.Text, ErrorMessage = "Max 25 tecken.")]
        [Display(Name = "Förnamn")]
        public string? FirstName { get; set; }
        [StringLength(25)]
        [DataType(DataType.Text,ErrorMessage = "Max 25 tecken.")]
        [Display(Name = "Efternamn")]
        public string? LastName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = false;

    }
}
