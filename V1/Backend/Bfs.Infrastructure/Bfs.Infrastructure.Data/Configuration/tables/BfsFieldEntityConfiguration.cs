using Bfs.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Infrastructure.Data.Configuration
{
    public class BfsFieldEntityConfiguration : IEntityTypeConfiguration<BfsFieldEntity>
    {
        public static readonly string TableNameCapital = "BfsField";

        public void Configure(EntityTypeBuilder<BfsFieldEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Field).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DisplayName).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
