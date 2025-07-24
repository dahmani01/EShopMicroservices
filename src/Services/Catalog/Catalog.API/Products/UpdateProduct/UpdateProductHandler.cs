using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required").MinimumLength(3)
            .WithMessage("Name must have at least 3 characters");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductResult> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProcutCommandHandler {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Store(product);
        await session.SaveChangesAsync();

        return new UpdateProductResult(true);
    }
}