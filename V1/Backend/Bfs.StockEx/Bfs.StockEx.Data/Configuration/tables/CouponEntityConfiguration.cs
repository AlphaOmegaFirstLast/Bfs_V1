using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.StockEx.Data.Configuration
{
    public class CouponEntityConfiguration : IEntityTypeConfiguration<CouponEntity>
    {
        public static readonly string TableNameCapital = "stkxCoupon";

        public void Configure(EntityTypeBuilder<CouponEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Value).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.AnnounceDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ValueDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DueDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.CouponPercent).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

