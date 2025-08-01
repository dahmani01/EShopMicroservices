﻿namespace Catalog.API.Products.GetProductById;

public record GetProductByIdRequest(Guid Id); 
public record GetProductByIdResponse(Product Product);

public class GeProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
         .WithName("GetProductById")
         .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Get a product by Id")
         .WithDescription("Gets a product By its Id");
    }
}
