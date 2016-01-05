using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentityDemo.Models
{
    public class Register
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Username")]
        [MinLength(5, ErrorMessage = "Your password must be atleast 6 characters long.")]
        [MaxLength(30, ErrorMessage = "Your password must be no longer than 16 characters long.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required]
        [MinLength(6, ErrorMessage = "Your password must be atleast 6 characters long.")]
        [MaxLength(16, ErrorMessage = "Your password must be no longer than 16 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

    }
}