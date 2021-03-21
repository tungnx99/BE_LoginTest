using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class SerachPaganationDTO<T>
    {
        public T Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
        public int Take
        {
            get
            {
                return PageNumber * PageSize;
            }
        }
        public int Skip
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
        }
    }
}
