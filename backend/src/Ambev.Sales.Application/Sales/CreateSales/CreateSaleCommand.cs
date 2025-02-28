using Ambev.DeveloperEvaluation.Common.Validation;
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
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {


        public Guid CustomerId { get;  set; }



        public Guid BranchId { get;  set; }

        public List<SaleItem> Items { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

    }
}
