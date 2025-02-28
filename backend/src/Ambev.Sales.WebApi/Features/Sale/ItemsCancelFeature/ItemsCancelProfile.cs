using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.Sales.Application.Sales.ItensCancel;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class ItemsCancelProfile: Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public ItemsCancelProfile()
    {
        CreateMap<ItensCancelRequest, CancelSaleItemCommand>();
    }
}
