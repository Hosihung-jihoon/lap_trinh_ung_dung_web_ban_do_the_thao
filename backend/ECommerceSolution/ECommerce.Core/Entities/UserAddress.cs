namespace ECommerce.Core.Entities
{
    public class UserAddress : BaseEntity
    {
        public int UserId { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;

        // Navigation property
        public virtual User User { get; set; } = null!;
    }
}
