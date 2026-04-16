using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Master.Data.Configuration
{
    public class SystemActionEntityConfiguration : IEntityTypeConfiguration<SystemActionEntity>
    {
        public static readonly string TableNameCapital = "SystemAction";

        public void Configure(EntityTypeBuilder<SystemActionEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ShortName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.MatchProperty).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.MatchValues).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ActionTemplate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
