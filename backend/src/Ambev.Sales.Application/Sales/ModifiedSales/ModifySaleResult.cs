using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class ModifySaleResult
    {
        public Guid Id { get; private set; }

        public string SaleNumber { get; private set; }

        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }

        public decimal TotalValue { get; private set; }

        public Guid DivisionId { get; private set; }

        public SaleStatus Status { get; private set; }

        public List<SaleItem> Items { get; private set; }
    }
}
