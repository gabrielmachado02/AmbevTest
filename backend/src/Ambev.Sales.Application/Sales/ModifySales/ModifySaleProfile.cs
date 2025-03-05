using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.Sales.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Application.ModifySales
{
    public class ModifySaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateUser operation
        /// </summary>
        public ModifySaleProfile()
        {
            CreateMap<ModifySaleCommand, Sale>();
            CreateMap<Sale, ModifySaleResult>();
        }
    }
}
