using FluentValidation;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleForEach(x => x.Items)
                        .Must(item => item.Quantity <= 20)
                        .WithMessage(item => $" No item can have more than 20 units");

        }
    }
}
