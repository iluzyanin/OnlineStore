using FluentValidation;
using OnlineStore.RestApi.DataTransfer;

public class ItemValidator : AbstractValidator<NewItemDto>
{
    public ItemValidator()
    {
        RuleFor(item => item.Name).NotEmpty().WithMessage("Please specify Item name.");
        RuleFor(item => item.Price).GreaterThan(0).WithMessage("Please specify positive Item price.");
    }
}