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
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

namespace Ambev.Sales.Unit.Tests.Sales
{
    public class CancelSaleCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly CancelSaleCommandHandle _handler;

        public CancelSaleCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CancelSaleCommandHandle(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldCancelSales_WhenSalesExist()
        {
            // Arrange
            var salesIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var command = new CancelSaleCommand { SalesIds = salesIds };

            var sales = salesIds.Select(id => new Sale { Id = id }).ToList();

            _saleRepository.GetByIdListAsync(salesIds, Arg.Any<CancellationToken>()).Returns(sales);
            _saleRepository.UpdateRangeAsync(Arg.Any<List<Sale>>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            await _saleRepository.Received(1).UpdateRangeAsync(sales, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSalesDoNotExist()
        {
            // Arrange
            var salesIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var command = new CancelSaleCommand { SalesIds = salesIds };

            _saleRepository.GetByIdListAsync(salesIds, Arg.Any<CancellationToken>()).Returns(new List<Sale>());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new CancelSaleCommand { SalesIds = new List<Guid>() };
            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            var saleList = new List<Sale>() { 
            new Sale()
            };
            _saleRepository.GetByIdListAsync(Arg.Any<List<Guid>>(), Arg.Any<CancellationToken>()).Returns(saleList);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            exception.Errors.Should().NotBeEmpty();
        }
    }
}
