using FrontendProject.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FrontendProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "rapidtech.id", ErrorMessage = "Domain Harus rapidtech.id")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Cinfirmation Password")]
        [Compare("Password", ErrorMessage = "Password dan Confirm password tidak sama")]
        public string ConfrimPassword { get; set; }

    }
}
