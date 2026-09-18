namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductQueryHandler (IDocumentSession session, ILogger<GetProductQueryHandler> logger):IQueryHandler<GetProductsQuery, GetProductsResult> 
{   
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductQueryHandler.Handle called with{@query}", query);
        var productsQuery = session.Query<Product>();
        var products = await Marten.QueryableExtensions.ToListAsync(productsQuery, cancellationToken);
        return new GetProductsResult(products);
    }
}
