using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloghost.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Your email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
