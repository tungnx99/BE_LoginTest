using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Auth;
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
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AccountController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
            _userService = userService;
        }
        // GET: api/<AccountController>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(Domain.Entities.User.GetList());
        //}

        [Route("list")]
        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] SerachPaganationDTO<UserDTO> userDTO)
        {
            return CommonResponse(0, _userService.GetUsers(userDTO));
        }

        [Route("item")]
        // GET api/<AccountController>/5
        [HttpGet]
        public IActionResult Get([FromQuery] string id)
        {
            return Ok(_userService.GetUser(id));
        }

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin data)
        {
            if (!ModelState.IsValid || data == null || string.IsNullOrWhiteSpace(data.UserName))
            {
                return CommonResponse(1, Constants.InvalidAuthInfoMsg);
            }
            try
            {
                var token = _authService.Login(data);
                return CommonResponse(0, token);
            }
            catch
            {
                return CommonResponse(1, Constants.InvalidAuthInfoMsg);
            }
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
