using System.ComponentModel.DataAnnotations;

namespace LibraryCult.Identity.API.Models
{
    public class UserViewModel 
    {

        public class UserRegisterViewModel
        {
            [EmailAddress]
            [Required(ErrorMessage = "The field {0} is required. Please, check again.")]
            public string Email { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "This fields {0} require minimum {2} and  maximum{1} caracteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password")]
            public string ConfirmPassword { get;set; }
        }

        public class UserLoginViewModel
        {
            [EmailAddress]
            [Required(ErrorMessage = "The field {0} is required. Please, check again.")]
            public string Email { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "This fields {0} require minimum {2} and  maximum{1} caracteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

    }
}
