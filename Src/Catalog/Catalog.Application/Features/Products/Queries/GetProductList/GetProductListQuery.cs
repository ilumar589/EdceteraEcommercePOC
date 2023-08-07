using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductList;

// For the query struct containing product name we have a read only list of BasicProductView type
public readonly record struct GetProductListQuery(string ProductName = "") : IRequest<IReadOnlyList<BasicProductView>>;