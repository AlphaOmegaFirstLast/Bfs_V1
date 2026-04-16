using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Master.Data.Configuration
{
    public class BfsComponentEntityConfiguration : IEntityTypeConfiguration<BfsComponentEntity>
    {
        public static readonly string TableNameCapital = "BfsComponent";

        public void Configure(EntityTypeBuilder<BfsComponentEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsSoftDelete).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DisplayName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.MenuName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.MenuPlaceHolder).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.QueryBaseTable).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.InterfaceRequired).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
