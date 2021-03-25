using Common.Paganation;
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
        public Paganation<Domain.Entities.Product> GetProducts(SerachPaganationDTO<ProductDTO> paganation);
        public void Upload(List<IFormFile> files, String namePath);
    }
}
