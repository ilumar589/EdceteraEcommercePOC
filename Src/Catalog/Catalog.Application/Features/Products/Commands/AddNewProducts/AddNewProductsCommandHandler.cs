using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Products.Commands.AddNewProducts;

public sealed class AddNewProductsCommandHandler : IRequestHandler<AddNewProductsCommand, IReadOnlyList<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<AddNewProductsCommandHandler> _logger;

    public AddNewProductsCommandHandler(IProductRepository productRepository, ILogger<AddNewProductsCommandHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Guid>> Handle(AddNewProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[AddNewProductsCommandHandler] Started creating products for request {@Request}", request);
        
        var productsToCreateTemplate = request.ProductCreateRequests;
        var productsToCreate = productsToCreateTemplate
            .Select(requestProduct => new Product
            {
                Name = requestProduct.ProductName,
                Description = requestProduct.ProductDescription,
                Type = ProductType.FromName(requestProduct.ProductType)
            }).ToList();

        var createdProductIds = await _productRepository.AddBulkAsync(productsToCreate);
        
        _logger.LogInformation("[AddNewProductsCommandHandler] Successfully created products {@ProductIds}", createdProductIds);
        
        return createdProductIds;
    }
}