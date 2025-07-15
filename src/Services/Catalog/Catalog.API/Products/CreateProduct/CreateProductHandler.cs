using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(); 
    }
}


