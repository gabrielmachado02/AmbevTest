using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Unit.Domain;
using Ambev.Sales.Application.ModifySales;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.Sales.Unit.Application
{
    public class ModifySaleHandleTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ModifySaleCommandHandle _handler;

        public ModifySaleHandleTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new ModifySaleCommandHandle(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = ModifySaleCommandTestData.GenerateValidCommand();
            var sale = new Sale(DateTime.UtcNow, command.CustomerId, command.BranchId);
            command.Items.ForEach(item => sale.AddItem(item.Name, item.Description, item.ProductId, item.Quantity, item.UnitPrice));

            var result = new ModifySaleResult { Id = sale.Id };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<ModifySaleResult>(sale).Returns(result);
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.Id.Should().Be(sale.Id);
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }
    }
}
