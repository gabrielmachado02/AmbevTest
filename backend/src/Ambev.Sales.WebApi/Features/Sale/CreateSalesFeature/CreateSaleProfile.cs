using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

namespace Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>(); 
    }
}
