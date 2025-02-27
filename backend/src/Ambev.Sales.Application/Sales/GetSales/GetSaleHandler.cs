using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.Sales.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class GetSaleHandler : IRequestHandler<GetUserCommand, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetUserCommand</param>
    public GetSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetUserCommand request
    /// </summary>
    /// <param name="request">The GetUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<GetSaleResult> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        return _mapper.Map<GetSaleResult>(user);
    }
}
