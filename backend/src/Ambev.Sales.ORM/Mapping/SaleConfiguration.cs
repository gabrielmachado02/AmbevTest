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
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");


            builder.Property(s => s.SaleNumber)
            
               .HasDefaultValueSql("LPAD((FLOOR(EXTRACT(EPOCH FROM clock_timestamp()) * 1000 + RANDOM() * 999)::BIGINT)::TEXT, 9, '0')");

            builder.HasIndex(s => s.SaleNumber).IsUnique();

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.TotalValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(u => u.Status)
          .HasConversion<string>()
          .HasMaxLength(20);

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
