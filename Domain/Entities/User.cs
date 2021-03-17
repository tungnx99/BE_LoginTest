using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class User
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

        static List<UserDTO> accounts;
        public static List<UserDTO> GetList()
        {
            if (accounts == null)
            {
                accounts = new List<UserDTO>();
                accounts.Add(new UserDTO() { UserName = "admin1", Password = "123456" });
                accounts.Add(new UserDTO() { UserName = "admin2", Password = "123456" });
                accounts.Add(new UserDTO() { UserName = "admin3", Password = "123456" });
                accounts.Add(new UserDTO() { UserName = "admin4", Password = "123456" });
                accounts.Add(new UserDTO() { UserName = "admin5", Password = "123456" });
                accounts.Add(new UserDTO() { UserName = "admin6", Password = "123456" });
            }
            return accounts;
        }
    }
}
