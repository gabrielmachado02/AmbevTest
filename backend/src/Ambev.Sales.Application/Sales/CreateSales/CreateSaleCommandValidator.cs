using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.SaleDate).NotEmpty();
            //RuleFor(sale => sale.TotalValue).NotEmpty();
            //RuleFor(user => user.Password).SetValidator(new PasswordValidator());
            //RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            //RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
            //RuleFor(user => user.Role).NotEqual(UserRole.None);
        }
    }
}
