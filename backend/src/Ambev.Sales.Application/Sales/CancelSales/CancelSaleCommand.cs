using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.Sales.Application.Sales.CreateSales;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CancelSaleCommand : IRequest<CancelSaleResponse>
    {


        public List<Guid> SalesIds { get;  set; }


        public ValidationResultDetail Validate()
        {
            var validator = new CancelSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

    }
}
