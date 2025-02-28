using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

/// <summary>
/// Represents the authentication request model for user login.
/// </summary>
public class ItensCancelRequest
{

    public Guid SaleId { get; set; }
    public List<Guid> saleItemIds { get;  set; }


}
