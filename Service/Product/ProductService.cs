using AutoMapper;
using Data;
using Domain.Common;
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
        public bool Delete(String id)
        {
            using (ShopDbContext shopDbContext = this.shopDbContext)
            {
                try
                {
                    var product = shopDbContext.Products.Where(i => i.Id == id).FirstOrDefault();
                    if (product == null)
                        return false;
                    if (Upload(null, product.Code).Result)
                    {
                        shopDbContext.Products.Remove(product);
                        shopDbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        public Paganation<Domain.Entities.Product> GetProducts(SerachPaganationDTO<ProductDTO> serachPaganationDTO)
        {
            using (ShopDbContext shopDbContext = this.shopDbContext)
            {
                if (serachPaganationDTO != null)
                {
                    var result = _mapper.Map<SerachPaganationDTO<ProductDTO>, Paganation<Domain.Entities.Product>>(serachPaganationDTO);
                    var data = shopDbContext.Products.ToList();
                    if (serachPaganationDTO.Search != null)
                    {
                        data = data.Where(t =>
                        t.Code.Contains(serachPaganationDTO.Search.Code ?? "") &&
                        t.Name.Contains(serachPaganationDTO.Search.Name ?? "")
                        ).OrderBy(t => t.Name).ThenBy(t => t.Code).ThenBy(t => t.Quantity).ToList();
                    }
                    result.Data = data.Take(serachPaganationDTO.Take).Skip(serachPaganationDTO.Skip).ToList();
                    result.TotalItems = data.Count;
                    return result;
                }
            }
            return new Paganation<Domain.Entities.Product>();
        }

        public Domain.Entities.Product GetProduct(String id)
        {
            using (ShopDbContext shopDbContext = this.shopDbContext)
            {
                var product = shopDbContext.Products.Find(id);
                if (product != null)
                    return product;
            }
            return new Domain.Entities.Product();
        }

        public bool Insert(ProductDTO model)
        {
            using (ShopDbContext shopDbContext = this.shopDbContext)
            {
                try
                {
                    var product = _mapper.Map<ProductDTO, Domain.Entities.Product>(model);
                    product.Id = Guid.NewGuid().ToString();
                    shopDbContext.Products.Add(product);
                    shopDbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        public bool Update(String id, ProductDTO model)
        {
            using (ShopDbContext shopDbContext = this.shopDbContext)
            {
                try
                {
                    var product = _mapper.Map<ProductDTO, Domain.Entities.Product>(model);
                    product.Id = id;
                    shopDbContext.Products.Update(product);
                    shopDbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> Upload(List<IFormFile> files, String namePath)
        {
            try
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
                            using (var stream = System.IO.File.Create(filePath + @"\" + formFile.FileName))
                            {
                                //stream.Write();
                                await formFile.CopyToAsync(stream);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
