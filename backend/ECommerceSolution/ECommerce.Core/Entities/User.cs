namespace ECommerce.Core.Entities
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? GoogleId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public string Role { get; set; } = "Customer";//Customer, Admin
        public bool IsLocked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Points { get; set; } = 0;

        //Navigation properties
        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }
}
