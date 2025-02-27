using FluentValidation;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(user => user.CartKey).NotEmpty();


        }
    }
}
