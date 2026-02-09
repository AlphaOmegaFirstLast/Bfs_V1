using Bfs.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Infrastructure.Data.Configuration
{
    public class BfsSystemEntityConfiguration : IEntityTypeConfiguration<BfsSystemEntity>
    {
        public static readonly string TableNameCapital = "BfsSystem";

        public void Configure(EntityTypeBuilder<BfsSystemEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.BasePortNumber).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DbPrefix).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
