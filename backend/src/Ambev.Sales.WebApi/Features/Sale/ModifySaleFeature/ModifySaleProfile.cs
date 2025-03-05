using Ambev.Sales.Application.ModifySales;
using Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;
using AutoMapper;

namespace Ambev.Sales.WebApi.Features.Sale.ModifySaleFeature;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class ModifySaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public ModifySaleProfile()
    {
        CreateMap<ModifySaleRequest, ModifySaleCommand>();
        CreateMap<ModifySaleResult, ModifySaleResponse>();
    }
}
