using System.Collections.Generic;

namespace ECommerce.Core.Entities
{
    public class ArticleCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}