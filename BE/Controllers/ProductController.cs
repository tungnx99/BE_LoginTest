using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Product;
using Service.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IRepository<ProductDTO> _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IRepository<ProductDTO> repository, IMapper mapper)
        {
            _productService = productService;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] SerachPaganationDTO<ProductDTO> serachPaganation)
        {
            IActionResult result;
            try
            {
                var products = _productService.GetProducts(serachPaganation);
                result = CommonResponse(0, products);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(1, Constants.Server.ErrorServer);
            }

            return result;
        }

        // POST api/<ProductController>
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public Task<IActionResult> Insert([FromForm] ProductDTO dto)
        {
            IActionResult result;
            try
            {
                _productService.Upload(dto.files, dto.Code);
                var product = _mapper.Map<ProductDTO, Domain.Entities.Product>(dto);
                _repository.Insert(dto);
                result = CommonResponse(0, Constants.Data.InsertSuccess);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(0, Constants.Server.ErrorServer);
            }
            return (Task<IActionResult>)result;
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        [Authorize]
        public Task<IActionResult> Update([FromForm] ProductDTO dto)
        {
            IActionResult result;
            try
            {
                _productService.Upload(dto.files, dto.Code);
                var product = _mapper.Map<ProductDTO, Domain.Entities.Product>(dto);
                _repository.Update(dto);
                result = CommonResponse(0, Constants.Data.UpdateSuccess);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(0, Constants.Server.ErrorServer);
            }
            return (Task<IActionResult>) result;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromQuery] String id)
        {
            IActionResult result;
            try
            {
                var product = _repository.Find(id);
                _productService.Upload(null, product.Code);
                _repository.Delete(id);
                result = CommonResponse(0, Constants.Data.DeleteSuccess);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(0, Constants.Server.ErrorServer);
            }
            return result;
        }
    }
}
