using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlgoUni.Models
{
    public class Membership
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email id is required")]

        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}