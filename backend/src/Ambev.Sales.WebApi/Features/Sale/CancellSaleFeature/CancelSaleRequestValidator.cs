using FluentValidation;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature
{
    public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
    {
        public CancelSaleRequestValidator()
        {
            RuleForEach(x => x.SalesIds).NotEmpty();

        }
    }
}
