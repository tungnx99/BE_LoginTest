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
        private readonly IRepository<Domain.Entities.Product> _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IRepository<Domain.Entities.Product> repository, IMapper mapper)
        {
            _productService = productService;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] SerachPaganationDTO<ProductDTOUpadate> serachPaganation)
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
        public async Task<IActionResult> Insert([FromForm] ProductDTO dto)
        {
            IActionResult result;
            try
            {
                var id = Guid.NewGuid().ToString();
                if (!await _productService.Upload(files: dto.files, namePath: id))
                {
                    throw new Exception(Constants.Data.UploadFail);
                }
                var product = _mapper.Map<ProductDTO, Domain.Entities.Product>(dto);
                _repository.Insert(product, id);
                result = CommonResponse(0, Constants.Data.InsertSuccess);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(0, Constants.Server.ErrorServer);
            }
            return result;
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ProductDTOUpadate dto)
        {
            IActionResult result;
            try
            {
                if (!await _productService.Upload(files: dto.files, namePath: dto.Id))
                {
                    throw new Exception(Constants.Data.UploadFail);
                }
                var product = _mapper.Map<ProductDTOUpadate, Domain.Entities.Product>(dto);
                _repository.Update(product);
                result = CommonResponse(0, Constants.Data.UpdateSuccess);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(0, Constants.Server.ErrorServer);
            }
            return result;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromForm] String id)
        {
            IActionResult result;
            try
            {
                if (!await _productService.Upload(files: null, namePath: id))
                {
                    throw new Exception(Constants.Data.UploadFail);
                }
                var product = _repository.Find(id);
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
