using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Konstnarer.Models.Register
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required, StringLength(25, MinimumLength = 4, ErrorMessage = "Behöver ett användarnamn med minst 4 tecken.")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Behöver en giltig epost.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Förnamn")]
        [StringLength(25)]
        public string? FirstName { get; set; }
        [Display(Name = "Efternamn")]
        [StringLength(25)]
        public string? LastName { get; set; }

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
