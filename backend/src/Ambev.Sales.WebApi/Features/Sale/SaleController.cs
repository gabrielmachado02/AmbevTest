using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.Sales.WebApi.Features.Sale.CreateSalesFeature;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.Sales.Application.Sales.CreateSales;
using Ambev.Sales.WebApi.Features.SaleItem.CancelSaleItem;
using Ambev.Sales.Application.Sales.ItensCancel;

namespace Ambev.Sales.WebApi.Features.Sale
{
    /// <summary>
    /// Controller for sale operations
    /// </summary>
    [ApiController]
    [Route("api/sales")]
    public class SaleController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of SaleController
        /// </summary>
        public SaleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a sale
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResponse>(response)
            });
        }

        /// <summary>
        /// Modifies a sale
        /// </summary>
        [HttpPut("modify")]
        [ProducesResponseType(typeof(ApiResponseWithData<ModifiedSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ModifySale([FromBody] ModifiedSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new ModifiedSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ModifySaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<ModifiedSaleResponse>
            {
                Success = true,
                Message = "Sale modified successfully",
                Data = _mapper.Map<ModifiedSaleResponse>(response)
            });
        }

        /// <summary>
        /// Cancels a sale
        /// </summary>
        [HttpPut("cancel")]
        [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelSale([FromBody] CancelSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CancelSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<CancelSaleResponse>
            {
                Success = true,
                Message = "Sale cancelled successfully",
                Data = response
            });
        }

        /// <summary>
        /// Cancels a specific sale item
        /// </summary>
        [HttpPut("cancel-item")]
        [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelSaleItem([FromBody] ItensCancelRequest request, CancellationToken cancellationToken)
        {
            var validator = new ItensCancelRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CancelSaleItemCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<CancelSaleItemResponse>
            {
                Success = true,
                Message = "Sales item cancelled successfully",
                Data = response
            });
        }
    }
}
