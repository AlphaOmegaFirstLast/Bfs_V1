using Bfs.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Infrastructure.Data.Configuration
{
    public class BfsClientSystemEntityConfiguration : IEntityTypeConfiguration<BfsClientSystemEntity>
    {
        public static readonly string TableNameCapital = "BfsClientSystem";

        public void Configure(EntityTypeBuilder<BfsClientSystemEntity> builder)
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

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

