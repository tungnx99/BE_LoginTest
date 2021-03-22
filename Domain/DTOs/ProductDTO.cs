using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs
{
    public class ProductDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Image
        {
            get
            {
                var result = "";
                if (files != null)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (i == 0)
                        {
                            result += files[i].FileName;
                            continue;
                        }
                        result += ";" + files[i].FileName;
                    }
                }
                return result;
            }
        }
        public List<IFormFile> files { get; set; }
    }
}
