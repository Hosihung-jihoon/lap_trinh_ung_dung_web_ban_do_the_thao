using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<UserAddress> UserAddresses { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Brand> Brands { get; private set; }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<ProductImage> ProductImages { get; private set; }
        public IGenericRepository<ProductVariant> ProductVariants { get; private set; }
        public IGenericRepository<MasterColor> MasterColors { get; private set; }
        public IGenericRepository<MasterSize> MasterSizes { get; private set; }
        public IGenericRepository<CartItem> CartItems { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<OrderDetail> OrderDetails { get; private set; }
        public IGenericRepository<OrderStatusHistory> OrderStatusHistories { get; private set; }
        public IGenericRepository<Payment> Payments { get; private set; }
        public IGenericRepository<Promotion> Promotions { get; private set; }
        public IGenericRepository<PromotionCondition> PromotionConditions { get; private set; }
        public IGenericRepository<ProductPromotion> ProductPromotions { get; private set; }
        public IGenericRepository<Coupon> Coupons { get; private set; }
        public IGenericRepository<ProductReview> ProductReviews { get; private set; }
        public IGenericRepository<Article> Articles { get; private set; }
        public IGenericRepository<ArticleCategory> ArticleCategories { get; private set; }
        public IGenericRepository<Notification> Notifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Users = new GenericRepository<User>(_context);
            UserAddresses = new GenericRepository<UserAddress>(_context);
            Categories = new GenericRepository<Category>(_context);
            Brands = new GenericRepository<Brand>(_context);
            Products = new GenericRepository<Product>(_context);
            ProductImages = new GenericRepository<ProductImage>(_context);
            ProductVariants = new GenericRepository<ProductVariant>(_context);
            MasterColors = new GenericRepository<MasterColor>(_context);
            MasterSizes = new GenericRepository<MasterSize>(_context);
            CartItems = new GenericRepository<CartItem>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderDetails = new GenericRepository<OrderDetail>(_context);
            OrderStatusHistories = new GenericRepository<OrderStatusHistory>(_context);
            Payments = new GenericRepository<Payment>(_context);
            Promotions = new GenericRepository<Promotion>(_context);
            PromotionConditions = new GenericRepository<PromotionCondition>(_context);
            ProductPromotions = new GenericRepository<ProductPromotion>(_context);
            Coupons = new GenericRepository<Coupon>(_context);
            ProductReviews = new GenericRepository<ProductReview>(_context);
            Articles = new GenericRepository<Article>(_context);
            ArticleCategories = new GenericRepository<ArticleCategory>(_context);
            Notifications = new GenericRepository<Notification>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}