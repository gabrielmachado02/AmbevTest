using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetSaleResult
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
