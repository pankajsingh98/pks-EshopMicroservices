namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Register endpoints here, e.g.:
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery(), CancellationToken.None);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
           .WithName("GetProducts")
           .Produces<GetProductsResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Get all products")
           .WithDescription("Retrieves a list of all products from the catalog.");
    }
}
