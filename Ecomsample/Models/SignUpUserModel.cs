using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomsample.Models
{
    public class SignUpUserModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse" , controller: "Account")]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public DateTime DateofBirth { get; set; }
    }
}
