using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.Sales.ORM;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;
        public SaleRepository(DefaultContext context)
        {
            _context = context;

        }
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Sale?> GetSaleByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(o => o.Id == userId, cancellationToken);
        }
    }
}
