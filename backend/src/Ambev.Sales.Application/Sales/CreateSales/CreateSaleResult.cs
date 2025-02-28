using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleResult
    {
        public Guid Id { get;  set; }

        public string SaleNumber { get;  set; }

        public DateTime SaleDate { get;  set; }

        public Guid CustomerId { get;  set; }

        public decimal TotalValue { get;  set; }

        public Guid DivisionId { get;  set; }

        public SaleStatus Status { get;  set; }

        public List<SaleItem> Items { get;  set; }
    }
}
