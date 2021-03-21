using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult Insert([FromBody] ProductDTO dto)
        {
            return Ok(_productService.Insert(dto));
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public ActionResult Update([FromQuery] string id, [FromBody] ProductDTO dto)
        {
            return Ok(_productService.Update(id, dto));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        public ActionResult Delete([FromQuery] String id)
        {
            return Ok(_productService.Delete(id));
        }
    }
}
