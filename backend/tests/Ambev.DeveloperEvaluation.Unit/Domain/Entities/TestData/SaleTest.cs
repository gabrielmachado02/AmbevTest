using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.Sales.Unit.Domain.Entities.TestData
{
    public class SaleTest
    {
        
        [Fact]
        public void Sale_ShouldInitializeCorrectly()
        {
            // Arrange
            var saleDate = DateTime.UtcNow;
            var customerId = Guid.NewGuid();
            var divisionId = Guid.NewGuid();

            // Act
            var sale = new Sale(saleDate, customerId, divisionId);

            // Assert
            Assert.NotNull(sale);
            Assert.Equal(saleDate, sale.SaleDate);
            Assert.Equal(customerId, sale.CustomerId);
            Assert.Equal(divisionId, sale.DivisionId);
            Assert.Equal(SaleStatus.Pending, sale.Status);
            Assert.NotNull(sale.Items);
            Assert.Empty(sale.Items);
        }

        [Fact]
        public void Sale_ShouldInitializeWithCustomStatus()
        {
            // Arrange
            var saleDate = DateTime.UtcNow;
            var customerId = Guid.NewGuid();
            var divisionId = Guid.NewGuid();
            var status = SaleStatus.Approved;

            // Act
            var sale = new Sale(saleDate, customerId, divisionId, status);

            // Assert
            Assert.Equal(status, sale.Status);
        }

        [Fact]
        public void AddItem_ShouldIncreaseItemsList()
        {
            // Arrange
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            sale.AddItem("Product 1", "Description", productId, 5, 10.00m);

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal(5, sale.Items.First().Quantity);
        }

        [Fact]
        public void CancelItem_ShouldSetItemStatusToCancelled()
        {
            // Arrange
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());
            var productId = Guid.NewGuid();

            sale.AddItem("Product 1", "Description", productId, 5, 10.00m);
            var itemId = sale.Items.First().Id;

            // Act
            sale.CancelItem(itemId);

            // Assert
            Assert.Equal(ItemStatus.Cancelled, sale.Items.First().ItemStatus);
        }

        [Fact]
        public void CancelItem_ShouldThrowException_WhenItemDoesNotExist()
        {
            // Arrange
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());
            var nonExistentItemId = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => sale.CancelItem(nonExistentItemId));
            Assert.Contains("não existe nesta venda", exception.Message);
        }

        [Fact]
        public void CalculateTotalValue_ShouldApplyCorrectDiscounts()
        {
            // Arrange
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());
            var productId = Guid.NewGuid();

            sale.AddItem("Product 1", "Description", productId, 5, 20.00m); // Total: 100.00m
            sale.AddItem("Product 2", "Description", productId, 10, 10.00m); // Total: 100.00m

            // Act
            var totalValue = sale.TotalValue;
            var discount = sale.Discount;

            // Assert
            Assert.Equal(200.00m, sale.TotalValue + sale.Discount); // Total antes do desconto
            Assert.Equal(40.00m, discount); // 20% de desconto
            Assert.Equal(160.00m, totalValue); // Total final após desconto
        }

        [Fact]
        public void CancelSale_ShouldSetStatusToCancelled()
        {
            // Arrange
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());

            // Act
            sale.Cancel();

            // Assert
            Assert.Equal(SaleStatus.Cancelled, sale.Status);
        }
    }
}

