namespace Ambev.Sales.WebApi.Features.SaleItem.CancelSaleItem
{
    public class CancelSaleItemRequest
    {
        public int SaleId { get; set; }
        public List<Guid> SalesIds { get; set; }

    }
}
