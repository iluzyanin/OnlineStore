using FluentValidation;
using OnlineStore.RestApi.DataTransfer;

namespace OnlineStore.RestApi.Validators
{
    public class CouponValidator : AbstractValidator<CouponDto>
    {
        public CouponValidator()
        {
            RuleFor(coupon => coupon.Code).NotEmpty().WithMessage("Please specify Coupon code.");
            RuleFor(coupon => coupon.Code).Length(10).WithMessage("Coupon code length must be 10.");
        }
    }
}