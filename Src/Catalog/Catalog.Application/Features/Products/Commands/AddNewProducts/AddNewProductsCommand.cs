using MediatR;

namespace Catalog.Application.Features.Products.Commands.AddNewProducts;

// command struct for creating new products with a return value of the new products guid
public readonly record struct AddNewProductsCommand(IReadOnlyList<BasicProductCreateRequest> ProductCreateRequests)
    : IRequest<IReadOnlyList<Guid>>;