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

namespace Ambev.Sales.Application.ModifySales
{
    public class ModifySaleCommandHandle : IRequestHandler<ModifySaleCommand, ModifySaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler
        /// </summary>
        /// <param name="SaleRepository">The Sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateSaleCommand</param>
        public ModifySaleCommandHandle(ISaleRepository SaleRepository, IMapper mapper)
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
        public async Task<ModifySaleResult> Handle(ModifySaleCommand command, CancellationToken cancellationToken)
        {

            var sale = new Sale(
              DateTime.Now.ToUniversalTime(),
              command.CustomerId,
              command.BranchId
          );

            foreach (var item in command.Items.ToList())
            {   
                sale.AddItem(item.Name,item.Description, item.ProductId, item.Quantity, item.UnitPrice);
            }

            var validator = new ModifySaleCommandValidator();

            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var createSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
            var result = _mapper.Map<ModifySaleResult>(createSale);
            return result;
        }
    }
}
