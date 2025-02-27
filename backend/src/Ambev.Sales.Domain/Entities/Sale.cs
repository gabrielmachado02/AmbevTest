using Ambev.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }

        public string SaleNumber { get; private set; }

        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }

        public decimal TotalValue { get; private set; }

        public Guid DivisionId { get; private set; }

        public SaleStatus Status { get; private set; }

        public List<SaleItem> Items { get; private set; }


        public Sale(string saleNumber, DateTime saleDate, Guid customerId, Guid divisionId)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerId = customerId;
            DivisionId = divisionId;
            Items = new List<SaleItem>();
        }

        public void AddItem(Guid productId, int quantity, decimal unitPrice, decimal discount)
        {
            var item = new SaleItem(Id, productId, quantity, unitPrice, discount);
            Items.Add(item);
            CalculateTotalValue();
        }


        private void CalculateTotalValue()
        {
            TotalValue = Items.Sum(item => item.TotalValue);
        }
    }
}