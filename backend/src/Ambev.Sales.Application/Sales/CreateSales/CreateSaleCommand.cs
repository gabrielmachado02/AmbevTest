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
    public class CreateSaleCommand : IRequest<CreateSaleCommandResult>
    {
        public Guid CartKey { get; private set; }

        public string SaleNumber { get; private set; }

        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }
        public Guid DivisionId { get; private set; }

        public SaleStatus Status { get; private set; }

        public List<SaleItem> Items { get; private set; }

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
