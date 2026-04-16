using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Master.Data.Configuration
{
    public class BfsTenantEntityConfiguration : IEntityTypeConfiguration<BfsTenantEntity>
    {
        public static readonly string TableNameCapital = "BfsTenant";

        public void Configure(EntityTypeBuilder<BfsTenantEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.DbConnection).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Theme).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.CompanyName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Logo).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
