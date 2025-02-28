using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CancelSaleProfile()
    {
        CreateMap<CancelSaleRequest, CancelSaleCommand>();
    }
}
