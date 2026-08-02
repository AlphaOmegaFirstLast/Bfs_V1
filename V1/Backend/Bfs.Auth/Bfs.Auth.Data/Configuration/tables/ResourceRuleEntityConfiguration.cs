using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Auth.Data.Configuration
{
    public class ResourceRuleEntityConfiguration : IEntityTypeConfiguration<ResourceRuleEntity>
    {
        public static readonly string TableNameCapital = "athResourceRule";

        public void Configure(EntityTypeBuilder<ResourceRuleEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.SelectBlackList).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.BfsComponentName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.JoinStatement).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.WhereStatement).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ParameterName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ParameterValue).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ParameterType).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.RoleName).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
