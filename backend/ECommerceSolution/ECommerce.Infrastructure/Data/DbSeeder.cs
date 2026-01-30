using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Check if data already exists
            if (await context.Users.AnyAsync())
                return;

            // Seed Admin User
            var admin = new User
            {
                Email = "admin@ecommerce.com",
                FullName = "Admin User",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };
            await context.Users.AddAsync(admin);

            // Seed Master Colors
            var colors = new List<MasterColor>
            {
                new() { Name = "Đen", HexCode = "#000000", SortOrder = 1 },
                new() { Name = "Trắng", HexCode = "#FFFFFF", SortOrder = 2 },
                new() { Name = "Đỏ", HexCode = "#FF0000", SortOrder = 3 },
                new() { Name = "Xanh Dương", HexCode = "#0000FF", SortOrder = 4 },
                new() { Name = "Xám", HexCode = "#808080", SortOrder = 5 }
            };
            await context.MasterColors.AddRangeAsync(colors);

            // Seed Master Sizes
            var sizes = new List<MasterSize>
            {
                new() { Name = "S", Type = "Clothing", SortOrder = 1 },
                new() { Name = "M", Type = "Clothing", SortOrder = 2 },
                new() { Name = "L", Type = "Clothing", SortOrder = 3 },
                new() { Name = "XL", Type = "Clothing", SortOrder = 4 },
                new() { Name = "XXL", Type = "Clothing", SortOrder = 5 }
            };
            await context.MasterSizes.AddRangeAsync(sizes);

            // Seed Categories
            var categories = new List<Category>
            {
                new() { Name = "Nam", Slug = "nam", IsActive = true, SortOrder = 1 },
                new() { Name = "Nữ", Slug = "nu", IsActive = true, SortOrder = 2 },
                new() { Name = "Trẻ Em", Slug = "tre-em", IsActive = true, SortOrder = 3 },
                new() { Name = "Phụ Kiện", Slug = "phu-kien", IsActive = true, SortOrder = 4 }
            };
            await context.Categories.AddRangeAsync(categories);

            // Seed Brands
            var brands = new List<Brand>
            {
                new() { Name = "Nike", Slug = "nike", IsActive = true },
                new() { Name = "Adidas", Slug = "adidas", IsActive = true },
                new() { Name = "Puma", Slug = "puma", IsActive = true },
                new() { Name = "Uniqlo", Slug = "uniqlo", IsActive = true }
            };
            await context.Brands.AddRangeAsync(brands);

            // Seed Article Categories
            var articleCategories = new List<ArticleCategory>
            {
                new() { Name = "Tin Tức", Slug = "tin-tuc", IsActive = true },
                new() { Name = "Hướng Dẫn", Slug = "huong-dan", IsActive = true },
                new() { Name = "Khuyến Mãi", Slug = "khuyen-mai", IsActive = true }
            };
            await context.ArticleCategories.AddRangeAsync(articleCategories);

            await context.SaveChangesAsync();
        }
    }
}