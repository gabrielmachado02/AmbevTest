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

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(c=>c.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<List<Sale>> GetByIdListAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
           return await _context.Sales
            .Where(s => ids.Contains(s.Id)) 
            .Include(s => s.Items) 
            .ToListAsync(cancellationToken);
        }
        public async Task UpdateRangeAsync(List<Sale> sales, CancellationToken cancellationToken)
        {
            _context.Sales.UpdateRange(sales);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Sale?> GetSaleByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(o => o.Id == userId, cancellationToken);
        }
    }
}
