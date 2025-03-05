using Ambev.Sales.Application.ModifySales;
using Ambev.Sales.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Unit.Application.TestData
{
    public static class ModifySaleCommandTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// The generated sales will have valid:
        /// - CustomerId (random GUID)
        /// - BranchId (random GUID)
        /// - Items (list of valid sale items)
        /// </summary>
        private static readonly Faker<ModifySaleCommand> modifySaleHandlerFaker = new Faker<ModifySaleCommand>()
            .RuleFor(s => s.CustomerId, f => f.Random.Guid())
            .RuleFor(s => s.BranchId, f => f.Random.Guid())
            .RuleFor(s => s.Items, f => ModifySaleItemsFaker.Generate(3));

        /// <summary>
        /// Generates a valid Sale entity with randomized data.
        /// The generated sale will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid Sale entity with randomly generated data.</returns>
        public static ModifySaleCommand GenerateValidCommand()
        {
            return modifySaleHandlerFaker.Generate();
        }
        public static class ModifySaleItemsFaker
        {
            private static readonly Faker<SaleItem> saleItemFaker = new Faker<SaleItem>()
                .RuleFor(i => i.Name, f => f.Commerce.ProductName())
                .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
                .RuleFor(i => i.ProductId, f => f.Random.Guid())
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 500));

            public static List<SaleItem> Generate(int count)
            {
                return saleItemFaker.Generate(count);
            }
        }

    }
}
