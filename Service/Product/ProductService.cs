using AutoMapper;
using Common.Paganation;
using Data;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        private ShopDbContext shopDbContext;
        private IMapper _mapper;
        public ProductService(IMapper mapper, ShopDbContext shopDbContext)
        {
            _mapper = mapper;
            this.shopDbContext = shopDbContext;
        }

        public Paganation<Domain.Entities.Product> GetProducts(SerachPaganationDTO<ProductDTOUpadate> panagation) // Todo: Wrong parameter name
        {
            if (panagation == null)
            {
                return new Paganation<Domain.Entities.Product>();
            }

            var data = shopDbContext.Products.Where(t =>
                panagation.Search == null ||
                (t.Code.Contains(panagation.Search.Code) &&
                t.Name.Contains(panagation.Search.Name))
                ).OrderBy(t => t.Name).ThenBy(t => t.Code).ThenBy(t => t.Quantity);

            var result = _mapper.Map<SerachPaganationDTO<ProductDTOUpadate>, Paganation<Domain.Entities.Product>>(panagation);
            var productdtos = data.Take(panagation.Take).Skip(panagation.Skip).ToList();

            result.InputData(totalItems: data.Count(), data: productdtos);
            return result;
        }

        public async void Upload(List<IFormFile> files, String namePath)
        {
            var filePath = Directory.GetCurrentDirectory() + @"\wwwroot\Images\Products\" + namePath;
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
            }
            if (files != null && files.Count > 0)
            {
                Directory.CreateDirectory(filePath);
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = File.Create(filePath + @"\" + formFile.FileName))
                        {
                            //stream.Write();
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
            }
        }
    }
}
