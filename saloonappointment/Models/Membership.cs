using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace saloonappointment.Models
{
    public class Membership
    {
        public int Id { get; set; }
        [Required]
        public string U_Firstname { get; set; }
        [Required]
        public string U_Lastname{ get; set; }
        [Required]
        public string U_Email { get; set; }
        [Required]
        public string U_Password { get; set; }
        [Required]
        public string U_gender { get; set; }
    }
}