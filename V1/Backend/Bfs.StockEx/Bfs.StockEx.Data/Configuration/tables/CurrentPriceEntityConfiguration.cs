using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.StockEx.Data.Configuration
{
    public class CurrentPriceEntityConfiguration : IEntityTypeConfiguration<CurrentPriceEntity>
    {
        public static readonly string TableNameCapital = "stkxCurrentPrice";

        public void Configure(EntityTypeBuilder<CurrentPriceEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.TransactionDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Price).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

