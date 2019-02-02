using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaStore.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required field!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required field!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}