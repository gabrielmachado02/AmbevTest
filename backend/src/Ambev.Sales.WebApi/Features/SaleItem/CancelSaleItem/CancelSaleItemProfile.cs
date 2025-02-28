using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;
using AutoMapper;

namespace Ambev.Sales.WebApi.Features.SaleItem.CancelSaleItem
{
    public class CancelSaleItemProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateUser feature
        /// </summary>
        public CancelSaleItemProfile()
        {
            CreateMap<ItensCancelRequest, ModifySaleCommand>();
            CreateMap<ModifySaleResult, CreateSaleResponse>();
        }
    }
}
