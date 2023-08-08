using System.Net;
using Catalog.Application.Features.Products.Commands.AddNewProducts;
using Catalog.Application.Features.Products.Queries.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "ReadProducts")]
    [HttpGet("{productName}", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<BasicProductView>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<BasicProductView>>> GetProductList(string productName)
    {
        var query = new GetProductListQuery
        {
            ProductName = productName
        };

        var products = await _mediator.Send(query);

        return Ok(products);
    }

    [HttpPost("AddNewProducts")]
    [ProducesResponseType(typeof(IReadOnlyList<Guid>), (int) HttpStatusCode.Created)]
    public async Task<ActionResult<IReadOnlyList<Guid>>> AddNewProducts([FromBody] AddNewProductsCommand addNewProductsCommand)
    {
        var createdProducts = await _mediator.Send(addNewProductsCommand);
        return Ok(createdProducts);
    }
}