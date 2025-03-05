using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.Sales.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Application.Sales.ItensCancel
{
    public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemCommand>
    {
        /// </remarks>
        /// 
        public CancelSaleItemValidator()
        {
                
        }
        private readonly ISaleRepository _saleRepository;
        public CancelSaleItemValidator(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;

            RuleFor(x => x.SaleId)
                .NotEmpty().WithMessage("O ID da venda é obrigatório.");

            RuleFor(x => x)
                .MustAsync(SaleExists)
                .WithMessage("Venda não encontrada.");

            RuleFor(x => x)
                .MustAsync(HasValidItems)
                .WithMessage("Nenhum dos itens especificados foi encontrado na venda.");
        }

        private async Task<bool> SaleExists(CancelSaleItemCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
            return sale != null;
        }

        private async Task<bool> HasValidItems(CancelSaleItemCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
            if (sale == null) return false;

            var itemsToRemove = sale.Items.Where(i => command.SaleItemIds.Contains(i.Id)).ToList();
            return itemsToRemove.Any();
        }
    }
}

