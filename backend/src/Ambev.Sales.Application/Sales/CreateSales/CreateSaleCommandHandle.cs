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
    public class CreateSaleCommandHandle : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of CreateUserHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateUserCommand</param>
        public CreateSaleCommandHandle(ISaleRepository userRepository, IMapper mapper)
        {
            _saleRepository = userRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Handles the CreateUserCommand request
        /// </summary>
        /// <param name="command">The CreateUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created user details</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {

            var sale = new Sale(
              DateTime.Now.ToUniversalTime(),
              command.CustomerId,
              command.BranchId
          );

            foreach (var item in command.Items.ToList())
            {
                sale.AddItem(item.Name, item.Description, item.ProductId, item.Quantity, item.UnitPrice);
            }

            var validator = new CreateSaleCommandValidator();

            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var createSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createSale);
            return result;
        }
    }
}
