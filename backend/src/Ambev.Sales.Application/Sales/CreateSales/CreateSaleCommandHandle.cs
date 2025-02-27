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
    internal class CreateSaleCommandHandle : IRequestHandler<CreateSaleCommand, CreateSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _redis;  
        /// <summary>
        /// Initializes a new instance of CreateUserHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateUserCommand</param>
        public CreateSaleCommandHandle(IConnectionMultiplexer redis,ISaleRepository userRepository, IMapper mapper)
        {
            _saleRepository = userRepository;
            _mapper = mapper;
        }

        public StackExchange.Redis.IDatabase Redis => _redis;

        /// <summary>
        /// Handles the CreateUserCommand request
        /// </summary>
        /// <param name="command">The CreateUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created user details</returns>
        public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var cartData = await Redis.StringGetAsync(command.CartKey.ToString());

            if (cartData.IsNullOrEmpty) throw new Exception("Cart  not found.");


         
            var sale = JsonConvert.DeserializeObject<Sale>(cartData);


            foreach (var item in sale.Items)
            {
                sale.AddItem(item.ProductId, item.Quantity, item.UnitPrice, item.Discount);
            }
            var validator = new CreateSaleCommandValidator();
         
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);




            var createdUser = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleCommandResult>(createdUser);
            return result;
        }
    }
}
