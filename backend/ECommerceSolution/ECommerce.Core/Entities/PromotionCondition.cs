namespace ECommerce.Core.Entities
{
    public class PromotionCondition : BaseEntity
    {
        public int PromotionId { get; set; }
        public string Field { get; set; } = string.Empty; // Category, Brand, TotalAmount, etc.
        public string Operator { get; set; } = string.Empty; // Equals, GreaterThan, LessThan, Contains
        public string Value { get; set; } = string.Empty;
        public string? LogicalOperator { get; set; } // AND, OR

        // Navigation property
        public virtual Promotion Promotion { get; set; } = null!;
    }
}