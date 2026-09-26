using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Configuration
{
    public class BfsSystemEntityConfiguration : IEntityTypeConfiguration<BfsSystemEntity>
    {
        public static readonly string TableNameCapital = "BfsSystem";
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public void Configure(EntityTypeBuilder<BfsSystemEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.IsMaster).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.BasePortNumber).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DbPrefix).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Logo).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        }
    }
}

