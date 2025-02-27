using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using System;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

/// <summary>
/// Represents the response returned after user authentication
/// </summary>
public sealed class CreateSaleResponse
{
    public string SaleNumber { get; private set; }

    public DateTime SaleDate { get; private set; }

    public Guid CustomerId { get; private set; }

    public decimal TotalValue { get; private set; }

    public Guid DivisionId { get; private set; }

    public SaleStatus Status { get; private set; }

    public List<SaleItem> Items { get; private set; }
}
