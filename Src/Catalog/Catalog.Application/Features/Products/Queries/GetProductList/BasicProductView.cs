namespace Catalog.Application.Features.Products.Queries.GetProductList;

public readonly record struct BasicProductView(Guid Id, string Name, string Description, string ProductType);