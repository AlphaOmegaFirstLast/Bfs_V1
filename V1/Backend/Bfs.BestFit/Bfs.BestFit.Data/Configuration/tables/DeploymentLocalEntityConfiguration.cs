using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.BestFit.Data.Configuration
{
    public class DeploymentLocalEntityConfiguration : IEntityTypeConfiguration<DeploymentLocalEntity>
    {
        public static readonly string TableNameCapital = "DeploymentLocal";

        public void Configure(EntityTypeBuilder<DeploymentLocalEntity> builder)
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
//   builder.Property(e => e.TargetVirtualFolder).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.WebSite).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.AppPoolName).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Port).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.HttpsRequired).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Project).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
