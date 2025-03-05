using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Application.ModifySales
{
    public class ModifySaleCommand : IRequest<ModifySaleResult>
    {

        public Guid Id { get; private set; }

        public Guid CustomerId { get; private set; }

        public Guid BranchId { get; private set; }

        public List<SaleItem> Items { get; private set; }

        public ValidationResultDetail Validate()
        {
            var validator = new ModifySaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

    }
}
