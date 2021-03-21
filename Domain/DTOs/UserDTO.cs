using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs
{
    public class UserDTO
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Type { get; set; }
    }

    public class UserLogin
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
