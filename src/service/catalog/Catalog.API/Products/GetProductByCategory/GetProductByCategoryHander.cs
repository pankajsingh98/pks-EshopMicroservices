namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>, IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
       logger.LogInformation("Handling GetProductByCategoryQuery for category: {Category}", query);

        var productsQuery = session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category));

        var products = await Marten.QueryableExtensions.ToListAsync(productsQuery, cancellationToken);

        return await Task.FromResult(new GetProductByCategoryResult(products));
    }
}
