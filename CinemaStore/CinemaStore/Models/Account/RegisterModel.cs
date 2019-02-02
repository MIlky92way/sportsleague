using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaStore.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required field!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required field!")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Do not math!")]
        public string ConfirmationPassword { get; set; }

        public string ReturnUrl { get; set; }
        public bool RememberMe { get; internal set; }
    }
}