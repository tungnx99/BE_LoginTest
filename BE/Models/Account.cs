using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public class Account
    {
        [Required]
        public String ID { get; set; }
        [Required]
        public DateTime BrithDay { get; set; }
        [Required]
        public String Sex { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
