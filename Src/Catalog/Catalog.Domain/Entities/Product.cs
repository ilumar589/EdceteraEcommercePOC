using Catalog.Domain.Common;

namespace Catalog.Domain.Entities;

public sealed class Product : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductType Type { get; set; }
}