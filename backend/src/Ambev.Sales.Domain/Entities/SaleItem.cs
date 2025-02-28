using Ambev.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get;  set; }
        public Guid SaleId { get;  set; }
        public Guid ProductId { get;  set; }
        public string Name { get; set; }
        public string Description  { get; set; }

        public int Quantity { get;  set; }
        public decimal UnitPrice { get;  set; }
        public decimal Discount { get;  set; }
        public ItemStatus ItemStatus { get; set; }
        public decimal TotalValue { get;  set; }

        public SaleItem() { } // Constructor for EF

        public SaleItem(string name ,string description, Guid saleId, Guid productId, int quantity, decimal unitPrice)
        {
            Name = name;
            Description = description;
            ItemStatus = ItemStatus.Active;
            Id = Guid.NewGuid();
            SaleId = saleId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CalculateTotalValue();
        }
        public void Cancel()
        {
            if (ItemStatus == ItemStatus.Cancelled)
                throw new InvalidOperationException($"O item {Id} já está cancelado.");

            ItemStatus = ItemStatus.Cancelled;
        }
        private void CalculateTotalValue()
        {
            TotalValue = (UnitPrice * Quantity) - Discount;
        }
    }
}
