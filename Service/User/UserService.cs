using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Users
{
    public class UserService : IUserService
    {
        public bool ILogin([FromBody] UserDTO data)
        {
            var reuult = false;
            var accounts = User.GetList();
            var account = accounts.Any(a => a.UserName == data.UserName && a.Password == data.Password);
            if (account)
                reuult = true;
            return reuult;
        }
    }
}
