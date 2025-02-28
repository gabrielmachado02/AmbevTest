using FluentValidation;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature
{
    public class ItensCancelRequestValidator : AbstractValidator<ItensCancelRequest>
    {
        public ItensCancelRequestValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty();
            RuleFor(x => x.saleItemIds).NotEmpty();


        }
    }
}
