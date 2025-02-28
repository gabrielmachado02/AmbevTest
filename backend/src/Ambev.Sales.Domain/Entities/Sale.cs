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

        public string SaleNumber { get; private set; } = GenerateSaleNumber();

        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }

        public decimal TotalValue { get; private set; }
        public decimal Discount { get; private set; }

        public Guid DivisionId { get; private set; }

        public SaleStatus Status { get; private set; }

        public List<SaleItem> Items { get; private set; }

        public Sale()
        {
                
        }
        public Sale(DateTime saleDate, Guid customerId, Guid divisionId)
        {
            Id = Guid.NewGuid();
            SaleDate = saleDate;
            CustomerId = customerId;
            Status = SaleStatus.Pending;
            DivisionId = divisionId;
            Items = new List<SaleItem>();
        }

        public Sale(DateTime saleDate, Guid customerId, Guid divisionId, SaleStatus status )
        {
            Id = Guid.NewGuid();
            SaleDate = saleDate;
            CustomerId = customerId;
            Status = status;
            DivisionId = divisionId;
            Items = new List<SaleItem>();

        }

        private static string GenerateSaleNumber()
        {
            return new Random().Next(100000000, 999999999).ToString();
        }

        public void AddItem(string name, string description,Guid productId, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(name,description, Id, productId, quantity, unitPrice);
            Items.Add(item);
            CalculateTotalAndDiscount();
            CalculateTotalValue();
        }

        public void Cancel()
        {
        
            Status = 0;
        }



        public void CancelItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
                throw new InvalidOperationException($"O item com Id {itemId} não existe nesta venda.");

            if (item.ItemStatus == ItemStatus.Cancelled)
                throw new InvalidOperationException($"O item com Id {itemId} já está cancelado.");

            item.Cancel(); // Método dentro de SaleItem para definir como "Cancelado"
        }
        private void CalculateTotalAndDiscount()
        {
            TotalValue = Items.Sum(i => i.TotalValue);
            int totalItems = Items.Sum(i => i.Quantity);

            if (totalItems >= 10 && totalItems <= 20)
            {
                Discount = TotalValue * 0.20m; 
            }
            else if (totalItems >= 4)
            {
                Discount = TotalValue * 0.10m; 
            }
            else
            {
                Discount = 0m;
            }
        }
        private void CalculateTotalValue()
        {
            TotalValue = Items.Sum(item => item.TotalValue)-Discount;
        }
    }
}