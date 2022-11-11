using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cooking_App.Models
{
    public class LoginProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="password and confirm password should be the safe")]
        public string ConfirmPassword { get; set; }
        public string PhotoName { get; set; }
    }
}