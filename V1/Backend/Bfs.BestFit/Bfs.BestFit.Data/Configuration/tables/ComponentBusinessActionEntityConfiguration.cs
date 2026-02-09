using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.BestFit.Data.Configuration
{
    public class ComponentBusinessActionEntityConfiguration : IEntityTypeConfiguration<ComponentBusinessActionEntity>
    {
        public static readonly string TableNameCapital = "ComponentBusinessAction";

        public void Configure(EntityTypeBuilder<ComponentBusinessActionEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}
