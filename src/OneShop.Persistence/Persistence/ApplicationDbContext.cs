using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneShop.Application.Common.Interfaces;
using OneShop.Domain.Entities;
using OneShop.Persistence.Extensions;

namespace OneShop.Persistence.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
                
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItemDetail> CartItemDetails { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ImageLocation> ImageLocations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<LookupTable> LookupTables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>().HasOne(c => c.Cart).WithOne().HasForeignKey<Customer>(c => c.CartId);
            builder.Entity<Order>().HasOne(o => o.Cart).WithOne().HasForeignKey<Order>(o => o.CartId);
            builder.Entity<Cart>().HasOne(u => u.Status).WithMany().HasForeignKey(lt => lt.StatusId);

            builder.Entity<CartItemDetail>().HasOne(cd => cd.Cart).WithMany(c => c.CartItemDetails).HasForeignKey(cd => cd.CartId);
            builder.Entity<CartItemDetail>().HasOne(cd => cd.Item).WithOne().HasForeignKey<CartItemDetail>(cd => cd.ItemId);
            builder.Entity<CartItemDetail>().HasOne(cd => cd.Vendor).WithOne().HasForeignKey<CartItemDetail>(cd => cd.VendorId);

            builder.Entity<Catalog>().HasOne(cg => cg.Vendor).WithMany(v => v.Catalogs).HasForeignKey(c => c.VendorId);

            builder.Entity<Item>().HasOne(i => i.CatalogInfo).WithMany(c => c.Items).HasForeignKey(i => i.CatalogId);
            builder.Entity<Item>().HasOne(u => u.ItemType).WithMany().HasForeignKey(lt => lt.ItemTypeId);
            builder.Entity<Item>().HasOne(u => u.Status).WithMany().HasForeignKey(lt => lt.StatusId);

            builder.Entity<ImageLocation>().HasOne<Customer>().WithMany(u => u.Images).HasForeignKey(il => il.ParentId);
            builder.Entity<ImageLocation>().HasOne<Item>().WithMany(u => u.Images).HasForeignKey(il => il.ParentId);
            builder.Entity<ImageLocation>().HasOne<Vendor>().WithMany(u => u.Images).HasForeignKey(il => il.ParentId);

            builder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);
            builder.Entity<Order>().HasOne(u => u.Status).WithMany().HasForeignKey(lt => lt.StatusId);

            builder.Entity<Customer>().HasOne(c => c.User).WithOne().HasForeignKey<Customer>(o => o.UserId);
            builder.Entity<Vendor>().HasOne(v => v.User).WithOne().HasForeignKey<Vendor>(o => o.UserId);

            builder.Entity<UserAddress>().HasOne(u => u.AddressType).WithMany().HasForeignKey(lt => lt.AddressTypeId);
            builder.Entity<UserAddress>().HasOne<User>().WithMany(u => u.Addresses).HasForeignKey(ua => ua.UserId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.UpdateEntityTimestamps();
            return SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }
    }
}
