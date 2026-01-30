using ECommerce.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<UserAddress> UserAddresses { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Brand> Brands { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<ProductImage> ProductImages { get; }
        IGenericRepository<ProductVariant> ProductVariants { get; }
        IGenericRepository<MasterColor> MasterColors { get; }
        IGenericRepository<MasterSize> MasterSizes { get; }
        IGenericRepository<CartItem> CartItems { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderDetail> OrderDetails { get; }
        IGenericRepository<OrderStatusHistory> OrderStatusHistories { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Promotion> Promotions { get; }
        IGenericRepository<PromotionCondition> PromotionConditions { get; }
        IGenericRepository<ProductPromotion> ProductPromotions { get; }
        IGenericRepository<Coupon> Coupons { get; }
        IGenericRepository<ProductReview> ProductReviews { get; }
        IGenericRepository<Article> Articles { get; }
        IGenericRepository<ArticleCategory> ArticleCategories { get; }
        IGenericRepository<Notification> Notifications { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}