using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.StockEx.Data.Configuration
{
    public class SspTransactionEntityConfiguration : IEntityTypeConfiguration<SspTransactionEntity>
    {
        public static readonly string TableNameCapital = "stkxSspTransaction";

        public void Configure(EntityTypeBuilder<SspTransactionEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.SourceDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.TransactionDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Source).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Quantity).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Price).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ToQuantity).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

