using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.BestFit.Data.Configuration
{
    public class DeploymentAzureStagingEntityConfiguration : IEntityTypeConfiguration<DeploymentAzureStagingEntity>
    {
        public static readonly string TableNameCapital = "DeploymentAzureStaging";

        public void Configure(EntityTypeBuilder<DeploymentAzureStagingEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.Project).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ScriptFile).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.SourceProject).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.SourcePath).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.PublishPath).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Config).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.EnvironmentValue).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.TargetVirtualFolder).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.PublishProfilePath).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.AppService).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ResourceGroup).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
