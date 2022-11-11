using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cooking_App.Models
{

    public class LoginProfile
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required]
        public int MobileNumber { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password and confirm password should be the safe")]
        public string ConfirmPassword { get; set; }
        public string PhotoName { get; set; }

    }
}