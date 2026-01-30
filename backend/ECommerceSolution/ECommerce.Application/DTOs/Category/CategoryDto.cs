namespace ECommerce.Application.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int SortOrder { get; set; }
        public int ProductCount { get; set; }
        public List<CategoryDto>? Children { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int SortOrder { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}