using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.Sales.Application.Sales.CreateSales;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Application.Sales.ItensCancel
{
    public class CancelSaleItemCommand : IRequest<CancelSaleItemResponse>
    {
        public Guid SaleId { get;  set; }

        public List<Guid> SaleItemIds { get;  set; }


        public ValidationResultDetail Validate()
        {
            var validator = new CancelSaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

    }
}
