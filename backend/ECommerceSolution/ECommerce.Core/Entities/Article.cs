using System;

namespace ECommerce.Core.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? Thumbnail { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishedAt { get; set; }
        public int Views { get; set; } = 0;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public virtual ArticleCategory Category { get; set; } = null!;
    }
}