using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.BestFit.Data.Configuration
{
    public class TableFieldEntityConfiguration : IEntityTypeConfiguration<TableFieldEntity>
    {
        public static readonly string TableNameCapital = "TableField";

        public void Configure(EntityTypeBuilder<TableFieldEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Field).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DisplayName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsQueryColumn).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsJoinField).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ParentTable).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.UiFormControlOrder).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
