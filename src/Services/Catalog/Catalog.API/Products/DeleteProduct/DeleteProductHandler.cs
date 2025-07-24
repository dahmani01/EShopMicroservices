using FluentValidation;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Id cannot be empty");
    }
}

internal class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<DeleteProductHandler> _logger;
    public DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger)
    {
        _session = session;
        _logger = logger;
    }
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteProductCommandHandler {@Command}", command);

        var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        _session.Delete(product);
        await _session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}