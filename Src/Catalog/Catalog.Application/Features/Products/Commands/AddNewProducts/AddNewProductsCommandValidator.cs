using FluentValidation;

namespace Catalog.Application.Features.Products.Commands.AddNewProducts;

public sealed class AddNewProductsCommandValidator : AbstractValidator<AddNewProductsCommand>
{
    public AddNewProductsCommandValidator()
    {
        RuleForEach(t => t.ProductCreateRequests).ChildRules(request =>
        {
            request.RuleFor(product => product.ProductName)
                .NotNull().WithMessage("Product name should not be empty")
                .NotEmpty().WithMessage("Product name should not be empty");

            request.RuleFor(product => product.ProductType)
                .NotNull();
        });
    }
}