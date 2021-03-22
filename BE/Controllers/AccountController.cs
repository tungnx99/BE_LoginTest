using AutoMapper;
using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: api/<AccountController>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(Domain.Entities.User.GetList());
        //}

        [Route("list")]
        [HttpGet]
        public IActionResult GetList([FromQuery] SerachPaganationDTO<UserDTO> userDTO)
        {
            return Ok(userService.GetUsers(userDTO));
        }

        [Route("item")]
        // GET api/<AccountController>/5
        [HttpGet]
        public IActionResult Get([FromQuery]string id)
        {
            return Ok(userService.GetUser(id));
        }

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin data)
        {
            return Ok(userService.Login(data));
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
