using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Honor x6c", Description = "Youtube Discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Pixel 9a", Description = "Youtube Discount", Amount = 100 });
    }
}