using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }

        protected SaleItem() { } // Constructor for EF

        public SaleItem(Guid saleId, Guid productId, int quantity, decimal unitPrice, decimal discount)
        {
            Id = Guid.NewGuid();
            SaleId = saleId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            CalculateTotalValue();
        }

        private void CalculateTotalValue()
        {
            TotalValue = (UnitPrice * Quantity) - Discount;
        }
    }
}
