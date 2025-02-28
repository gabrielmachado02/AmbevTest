using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

public class CancelSaleRequest
{
    public List<Guid> SalesIds { get;  set; }

 }
