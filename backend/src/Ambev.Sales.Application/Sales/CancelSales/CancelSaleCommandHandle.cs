using Ambev.Sales.Application.Sales.CreateSales;
using Ambev.Sales.Domain.Dto;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CancelSaleCommandHandle : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler
        /// </summary>
        /// <param name="SaleRepository">The Sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateSaleCommand</param>
        public CancelSaleCommandHandle(ISaleRepository SaleRepository, IMapper mapper)
        {
            _saleRepository = SaleRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Handles the CreateSaleCommand request
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created Sale details</returns>
        public async Task<CancelSaleResponse> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {

            var sales = await _saleRepository.GetByIdListAsync(command.SalesIds, cancellationToken);

            if (sales == null || !sales.Any())
                throw new Exception("Nenhuma venda encontrada para os IDs fornecidos.");

            foreach (var sale in sales)
            {
                sale.Cancel(); 
            }

            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _saleRepository.UpdateRangeAsync(sales, cancellationToken);

            return new CancelSaleResponse { Success = true };

        }
    }
}
