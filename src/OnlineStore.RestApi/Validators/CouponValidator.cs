using FluentValidation;
using OnlineStore.RestApi.DataTransfer;

namespace OnlineStore.RestApi.Validators
{
    public class CouponValidator : AbstractValidator<CouponDto>
    {
        public CouponValidator()
        {
            RuleFor(coupon => coupon.Coupon).NotEmpty().WithMessage("Please specify Coupon.");
            RuleFor(coupon => coupon.Coupon).Length(10).WithMessage("Coupon length must be 10.");
        }
    }
}