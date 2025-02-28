using Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;
using FluentValidation;

namespace Ambev.Sales.WebApi.Features.SaleItem.CancelSaleItem
{
    public class CancelSaleItemValidator : AbstractValidator<CancelSaleRequest>
    {
        public CancelSaleItemValidator()
        {
            RuleForEach(x => x.SalesIds).NotEmpty();

        }
    }
}
