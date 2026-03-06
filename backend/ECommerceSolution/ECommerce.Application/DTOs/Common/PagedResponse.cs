using System;
using System.Collections.Generic;

namespace ECommerce.Application.DTOs.Common
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(IEnumerable<T> data, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            TotalRecords = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
