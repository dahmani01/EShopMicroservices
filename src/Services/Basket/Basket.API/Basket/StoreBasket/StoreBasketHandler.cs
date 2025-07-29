namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);


public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null.");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
        RuleFor(x => x.Cart.Items).NotEmpty().WithMessage("At least one item is required in the cart.");
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    private readonly IBasketRepository _basketRepository;
    public StoreBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await _basketRepository.StoreBasketAsync(command.Cart);
        return new StoreBasketResult(result);
    }
}