using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.StockEx.Data.Configuration
{
    public class CustomReportsEntityConfiguration : IEntityTypeConfiguration<CustomReportsEntity>
    {
        public static readonly string TableNameCapital = "stkxCustomReports";

        public void Configure(EntityTypeBuilder<CustomReportsEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Request).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.BaseReport).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsPrivate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.CreatedBy).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Url).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

