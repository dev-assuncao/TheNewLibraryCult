﻿using System.ComponentModel.DataAnnotations;

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

        public class UserResponseViewModel
        {
            public bool Status { get; set; }
            public int StatusCode { get; set; }
            public string AccessToken { get; set; }
            public double ExpiresIn { get; set; }
            public UserToken UserToken { get; set; }
            public IEnumerable<string> Errors { get; set; }
            public string Message { get; set; }
        }

        public class UserToken
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public IEnumerable<UserClaimsViewModel> Claims { get; set; }
        }

        public class UserClaimsViewModel
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }

    }
}
