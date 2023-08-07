namespace Catalog.Application.Features.Products.Commands.AddNewProducts;

// string ProductType is a great case for showing how api level exceptions will be handled
public readonly record struct BasicProductCreateRequest(string ProductName, string ProductDescription, string ProductType);