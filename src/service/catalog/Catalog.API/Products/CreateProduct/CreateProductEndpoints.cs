
namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);
public class CreateProductEndpoints : ICarterModule
{
    // minimal implementation so the class compiles
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Register endpoints here
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // Simulate product creation logic
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        }).WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Creates a new product")
        .WithDescription("Creates a new product with the specified details and returns the created product's ID.");

    }
}
