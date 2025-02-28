using Ambev.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Sales.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(si => si.Id);

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();



            builder.Property(si => si.Discount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(u => u.ItemStatus)
         .HasConversion<string>()
         .HasMaxLength(20);

            builder.Property(si => si.TotalValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}


