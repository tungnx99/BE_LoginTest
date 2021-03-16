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
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
