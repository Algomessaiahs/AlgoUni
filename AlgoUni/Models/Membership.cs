using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlgoUni.Models
{
    public class Membership
    {
        [DisplayName("Email Id")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email id is required")]

        public string EmailId { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}