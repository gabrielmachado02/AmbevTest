using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.Domain.Dto
{
    public class SaleDto
    {
        public Guid CustomerId { get; set; }
        public Guid DivisionId { get; set; }
        public List<SaleItem> Items { get; set; }
    }
}
