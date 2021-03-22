using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Product
{
    public interface IProductService
    {
        public bool Insert(ProductDTO model);
        public bool Update(String id,ProductDTO model);
        public bool Delete(String id);
        public Paganation<Domain.Entities.Product> GetProducts(SerachPaganationDTO<ProductDTO> serachPaganationDTO);
        public Domain.Entities.Product GetProduct(String id);
        public Task<Boolean> Upload(List<IFormFile> files, String namePath);
    }
}
