using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.BestFit.Data.Configuration
{
    public class SystemInfoEntityConfiguration : IEntityTypeConfiguration<SystemInfoEntity>
    {
        public static readonly string TableNameCapital = "SystemInfo";

        public void Configure(EntityTypeBuilder<SystemInfoEntity> builder)
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

        }
    }
}
