using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Master.Data.Configuration
{
    public class DeploymentAzureEntityConfiguration : IEntityTypeConfiguration<DeploymentAzureEntity>
    {
        public static readonly string TableNameCapital = "DeploymentAzure";

        public void Configure(EntityTypeBuilder<DeploymentAzureEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ScriptFile).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.SourceProject).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.SourcePath).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.PublishPath).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Config).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.EnvironmentValue).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.TargetVirtualDir).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.PublishProfilePath).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.AppService).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ResourceGroup).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
