using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductList;

public sealed class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<BasicProductView>>
{
    private readonly IProductRepository _productRepository;

    public GetProductListQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<BasicProductView>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var productsByName = await _productRepository.GetProductsWithSimilarName(request.ProductName);

        return productsByName
            .Select(pbn => new BasicProductView
            {
                Id = pbn.Id,
                Name = pbn.Name,
                Description = pbn.Description,
                ProductType = ProductType.From(pbn.GetProductTypeId()).Name
            }).ToList();
    }
}