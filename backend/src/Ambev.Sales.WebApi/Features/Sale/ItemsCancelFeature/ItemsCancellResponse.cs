using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using System;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

/// <summary>
/// Represents the response returned after user authentication
/// </summary>
public sealed class ItemsCancellResponse
{
    public bool Success { get; set; }

}
