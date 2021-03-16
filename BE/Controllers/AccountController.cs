using BE.DTO;
using BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        // GET: api/<AccountController>
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(GetALL());
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(GetALL()[id]);
        }

        // POST api/<AccountController>
        [HttpPost]
        public JsonResult Post([FromBody] AccountDTO data)
        {
            List<AccountDTO> accounts = GetALL();
            if (accounts.Find(a=> a.UserName == data.UserName && a.Password == data.Password) == null)
            {
                return new JsonResult(false);
            }
            return new JsonResult(true);
        }

        private List<AccountDTO> accounts;
        public List<AccountDTO> GetALL()
        {
            if(this.accounts == null)
            {
                accounts = new List<AccountDTO>();
                accounts.Add(new AccountDTO() { UserName = "admin1", Password = "123456" });
                accounts.Add(new AccountDTO() { UserName = "admin2", Password = "123456" });
                accounts.Add(new AccountDTO() { UserName = "admin3", Password = "123456" });
                accounts.Add(new AccountDTO() { UserName = "admin4", Password = "123456" });
                accounts.Add(new AccountDTO() { UserName = "admin5", Password = "123456" });
                accounts.Add(new AccountDTO() { UserName = "admin6", Password = "123456" });
            }
            return accounts;
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
