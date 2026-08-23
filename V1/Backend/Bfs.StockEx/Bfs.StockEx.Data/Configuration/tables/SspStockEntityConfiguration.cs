using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.StockEx.Data.Configuration
{
    public class SspStockEntityConfiguration : IEntityTypeConfiguration<SspStockEntity>
    {
        public static readonly string TableNameCapital = "stkxSspStock";

        public void Configure(EntityTypeBuilder<SspStockEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Quantity).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.AverageCost).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

