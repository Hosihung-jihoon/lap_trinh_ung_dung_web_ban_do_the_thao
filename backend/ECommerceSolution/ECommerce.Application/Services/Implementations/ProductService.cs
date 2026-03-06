using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Services.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductDto>> GetProductsAsync(ProductQueryParameters queryParameters)
        {
            var query = _unitOfWork.Products.GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .Include(p => p.Variants)
                .AsNoTracking();

            // Filters
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                var search = queryParameters.SearchTerm.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(search) || 
                                         (p.Description != null && p.Description.ToLower().Contains(search)));
            }

            if (queryParameters.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == queryParameters.CategoryId.Value);
            }

            if (queryParameters.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == queryParameters.BrandId.Value);
            }

            if (queryParameters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= queryParameters.MinPrice.Value);
            }

            if (queryParameters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= queryParameters.MaxPrice.Value);
            }

            if (queryParameters.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == queryParameters.IsActive.Value);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                switch (queryParameters.SortBy.ToLower())
                {
                    case "price":
                        query = queryParameters.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                        break;
                    case "name":
                        query = queryParameters.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    case "date":
                        query = queryParameters.IsDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                        break;
                    case "views":
                        query = queryParameters.IsDescending ? query.OrderByDescending(p => p.Views) : query.OrderBy(p => p.Views);
                        break;
                    case "sales":
                        query = queryParameters.IsDescending ? query.OrderByDescending(p => p.SoldCount) : query.OrderBy(p => p.SoldCount);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.CreatedAt);
            }

            var totalRecords = await query.CountAsync();

            var products = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            var mappedData = _mapper.Map<IEnumerable<ProductDto>>(products);

            return new PagedResponse<ProductDto>(mappedData, totalRecords, queryParameters.PageNumber, queryParameters.PageSize);
        }

        public async Task<ProductDto?> GetProductBySlugAsync(string slug)
        {
            var product = await _unitOfWork.Products.GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .Include(p => p.Variants).ThenInclude(v => v.Color)
                .Include(p => p.Variants).ThenInclude(v => v.Size)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);

            if (product == null) return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductVariantDto>> GetProductVariantsAsync(int productId)
        {
            var variants = await _unitOfWork.ProductVariants.GetQueryable()
                .Include(v => v.Color)
                .Include(v => v.Size)
                .Where(v => v.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductVariantDto>>(variants);
        }

        public async Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync(int count = 10)
        {
            var products = await _unitOfWork.Products.GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.Views)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetNewArrivalsAsync(int count = 10)
        {
            var products = await _unitOfWork.Products.GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetBestSellersAsync(int count = 10)
        {
            var products = await _unitOfWork.Products.GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.SoldCount)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetRelatedProductsAsync(int productId, int count = 5)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null) return Enumerable.Empty<ProductDto>();

            var products = await _unitOfWork.Products.GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .Where(p => p.CategoryId == product.CategoryId && p.Id != productId && p.IsActive)
                .OrderBy(_ => Guid.NewGuid()) // Random order, or can orderBy views/date
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
