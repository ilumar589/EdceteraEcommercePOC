using Catalog.Domain.Common;
using Catalog.Domain.Exceptions;

namespace Catalog.Domain.Entities;

public sealed class ProductType : Enumeration
{
    public static ProductType Course = new(1, nameof(Course).ToLowerInvariant());
    public static ProductType Forum = new(1, nameof(Forum).ToLowerInvariant());
    public static ProductType Webinar = new(1, nameof(Webinar).ToLowerInvariant());
    
    public ProductType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<ProductType> List() => new[] { Course, Forum, Webinar };

    public static ProductType FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new CatalogDomainException(
                $"Possible values for product type: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static ProductType From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);
        
        if (state == null)
        {
            throw new CatalogDomainException(
                $"Possible values for product type: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}