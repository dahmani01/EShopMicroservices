namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>; 

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdResult> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductById query called {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product); 
    }
}
