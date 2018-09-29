using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Your name is not specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your email is not specified")]
        [EmailAddress(ErrorMessage ="Incorrect email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password entered incorrectly")]
        public string ConfirmPassword { get; set; }
    }
}
