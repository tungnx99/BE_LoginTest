using AutoMapper;
using Data;
using Domain.Common;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            try
            {
                var product = shopDbContext.Products.Where(i => i.Id == id).FirstOrDefault();
                if (product == null)
                    return false;
                shopDbContext.Products.Remove(product);
                shopDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public Paganation<Domain.Entities.Product> GetProducts(SerachPaganationDTO<ProductDTO> serachPaganationDTO)
        {
            if (serachPaganationDTO != null)
            {
                var result = _mapper.Map<SerachPaganationDTO<ProductDTO>, Paganation<Domain.Entities.Product>>(serachPaganationDTO);
                var data = shopDbContext.Products.Where(t =>
                    t.Code.Contains(serachPaganationDTO.Search.Code) &&
                    t.Name.Contains(serachPaganationDTO.Search.Name) &&
                    t.Quantity.Contains(serachPaganationDTO.Search.Quantity)
                    ).OrderBy(t => t.Name).ThenBy(t => t.Code).ThenBy(t => t.Quantity).ToList();
                result.Data = data.Take(serachPaganationDTO.Take).Skip(serachPaganationDTO.Skip).ToList();
                result.TotalItems = data.Count;
                return result;
            }
            return new Paganation<Domain.Entities.Product>();
        }

        public Domain.Entities.Product GetProduct(String id)
        {
            var product = shopDbContext.Products.Where(t => t.Id == id).FirstOrDefault();
            if (product != null)
                return product;
            return new Domain.Entities.Product();
        }

        public bool Insert(ProductDTO model)
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
            return true;
        }

        public bool Update(String id, ProductDTO model)
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
            return true;
        }
    }
}
