using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Configuration
{
    public class CustomFieldDefinitionEntityConfiguration : IEntityTypeConfiguration<CustomFieldDefinitionEntity>
    {
        public static readonly string TableNameCapital = "CustomFieldDefinition";
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public void Configure(EntityTypeBuilder<CustomFieldDefinitionEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.DisplayName).HasMaxLength([FieldLength]).IsRequired();

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        }
    }
}

