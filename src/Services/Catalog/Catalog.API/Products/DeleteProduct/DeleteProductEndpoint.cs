namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                var response = new DeleteProductResponse(result.IsSuccess);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(response);
            }).WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<DeleteProductResponse>(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete an existing product");
    }
}