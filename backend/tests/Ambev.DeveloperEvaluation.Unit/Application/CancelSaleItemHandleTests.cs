using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Application.Sales.ItensCancel;

namespace Ambev.Sales.Unit.Tests.Sales
{
    public class CancelSaleItemHandleTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly CancelSaleItemCommandHandle _handler;
        private readonly CancelSaleItemValidator _validator;
        private readonly IMapper _mapper;

        public CancelSaleItemHandleTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();

            _validator = new CancelSaleItemValidator(_saleRepository);
            _handler = new CancelSaleItemCommandHandle(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldCancelSaleItems_WhenSaleExistsAndItemsAreValid()
        {
            var saleId = Guid.NewGuid();
            var saleItemId = Guid.NewGuid();
            var ids = new List<Guid>();
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());


            sale.AddItem("Produto Teste", "Descrição", saleItemId, 2, 100);
            foreach (var item in sale.Items)
            {
                ids.Add(item.Id);

            }
            var command = new CancelSaleItemCommand
            {
                SaleId = saleId,
                SaleItemIds = ids
            };
            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Sucess.Should().BeTrue();
            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new CancelSaleItemCommand { SaleId = Guid.Empty, SaleItemIds = new List<Guid>() };
            var validationResult = await _validator.ValidateAsync(command, CancellationToken.None);

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid()));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            exception.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Validator_ShouldReturnError_WhenSaleDoesNotExist()
        {
            // Arrange
            var command = new CancelSaleItemCommand { SaleId = Guid.NewGuid(), SaleItemIds = new List<Guid> { Guid.NewGuid() } };
            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>()).Returns((Sale)null);

            // Act
            var validationResult = await _validator.ValidateAsync(command, CancellationToken.None);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "Venda não encontrada.");
        }

        [Fact]
        public async Task Validator_ShouldReturnError_WhenNoValidItemsInSale()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid());
            var command = new CancelSaleItemCommand { SaleId = saleId, SaleItemIds = new List<Guid> { Guid.NewGuid() } };

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

            // Act
            var validationResult = await _validator.ValidateAsync(command, CancellationToken.None);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "Nenhum dos itens especificados foi encontrado na venda.");
        }
    }
}
