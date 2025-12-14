using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : Grpc.DiscountService.DiscountServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var productName = request.ProductName;
        if (!string.IsNullOrEmpty(productName))
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(p => p.ProductName == productName);
            if (coupon != null)
            {
                logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}",
                    coupon.ProductName, coupon.Amount);
                return coupon.Adapt<CouponModel>();
            }

            return new CouponModel { ProductName = "No Discount for this product", Amount = 0, Description = "" };
        }

        throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid productName"));
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null || coupon.Amount <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon.Amount <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var productName = request.ProductName;

        if (string.IsNullOrEmpty(productName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid productName"));

        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(p => p.ProductName == productName);

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid product with name {productName}"));

        dbContext.Coupons.Remove(coupon);
        logger.LogInformation("Discount deleted for ProductName : {ProductName}",
            coupon.ProductName);
        await dbContext.SaveChangesAsync();

        return new DeleteDiscountResponse { Success = true };
    }
}