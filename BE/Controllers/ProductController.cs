﻿using Common.Paganation;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Product;
using System;
using System.Collections.Generic;
using System.IO;
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
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ProductController>/5
        //[HttpGet]
        //public ActionResult Get([FromQuery] String id)
        //{
        //    return Ok(_productService.GetProduct(id));
        //}

        [HttpGet]
        public ActionResult GetProducts([FromQuery] SerachPaganationDTO<ProductDTO> serachPaganation)
        {
            var products = _productService.GetProducts(serachPaganation);
            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Insert([FromForm] ProductDTO dto)
        {
            bool isUploadOk = await _productService.Upload(dto.files, dto.Code);
            if (isUploadOk)
            {
                bool isInsertOk = _productService.Insert(dto);
                return base.Ok(isInsertOk);
            }
            return Ok(false);
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] string id, [FromForm] ProductDTO dto)
        {
            // Wrap if with braces or use conditional expression
            if (!await _productService.Upload(dto.files, dto.Code))
                return Ok(false);
            return Ok(_productService.Update(id, dto));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        public ActionResult Delete([FromQuery] String id)
        {
            try
            {
                bool isDeleteOk = _productService.Delete(id);
                return base.Ok(isDeleteOk);
            }
            catch (Exception ex)
            {
                // Write log for ex
                return Ok(false);
            }
        }

        //[Route("image")]
        //[HttpGet]
        //public ActionResult GetImage([FromQuery] int image)
        //{
        //    return Ok(true);
        //}
    }
}
