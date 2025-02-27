using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Validator for GetSaleCommand
/// </summary>
public class GetSaleValidator : AbstractValidator<GetUserCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSaleCommand
    /// </summary>
    public GetSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
