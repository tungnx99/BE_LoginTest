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
        UserService userService;
        public AccountController()
        {
            userService = new UserService();
        }
        // GET: api/<AccountController>
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Domain.Entities.User.GetList());
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(Domain.Entities.User.GetList()[id]);
        }

        // POST api/<AccountController>
        [HttpPost]
        public JsonResult Post([FromBody] UserDTO data)
        {
            return new JsonResult(userService.ILogin(data));
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
