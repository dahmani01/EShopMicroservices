using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(ShoppingCart Cart);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null.");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
        RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("At least one item is required in the cart.");
    }
}

public class StoreBasketCommandHandler(IBasketRepository basketRepository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await basketRepository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(result);
    }
}