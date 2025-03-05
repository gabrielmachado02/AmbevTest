using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;

namespace Ambev.Sales.WebApi.Features.Sale.ModifySaleFeature;

/// <summary>
/// Represents the authentication request model for user login.
/// </summary>
public class ModifySaleRequest
{
    /// <summary>
    /// Gets or sets the password. Must meet security requirements.
    /// </summary>

    public Guid CustomerId { get;  set; }

    public Guid DivisionId { get; set; }

    public List<Domain.Entities.SaleItem> Items { get; set; }

}
