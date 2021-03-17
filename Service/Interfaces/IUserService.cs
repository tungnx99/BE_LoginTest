using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Users
{
    public interface IUserService
    {
        public bool ILogin([FromBody] UserDTO data);
    }
}
