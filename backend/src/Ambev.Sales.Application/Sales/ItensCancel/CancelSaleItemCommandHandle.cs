using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.Sales.Application.Sales.CreateSales;
using Ambev.Sales.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Application.Sales.ItensCancel
{
    public class CancelSaleItemCommandHandle : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResponse>
    {

        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

      
        public CancelSaleItemCommandHandle(ISaleRepository SaleRepository, IMapper mapper)
        {
            _saleRepository = SaleRepository;
            _mapper = mapper;
        }
        public async Task<CancelSaleItemResponse> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
            var failures = new List<ValidationFailure>();


            var validator = new CancelSaleItemValidator(_saleRepository);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var itemsToRemove = sale.Items.Where(i => command.SaleItemIds.Contains(i.Id)).ToList();


            foreach (var item in itemsToRemove)
            {
                sale.CancelItem(item.Id);
            }

      


            // Atualiza a venda no banco
            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return new CancelSaleItemResponse { Sucess = true };

        }

    }
}
