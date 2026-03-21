using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Auth.Data.Configuration
{
    public class AuthUserEntityConfiguration : IEntityTypeConfiguration<AuthUserEntity>
    {
        public static readonly string TableNameCapital = "AuthUser";

        public void Configure(EntityTypeBuilder<AuthUserEntity> builder)
        {
            builder.ToTable(TableNameCapital);
            builder.HasKey(e => e.Id);

            // Explicitly disable identity generation
            builder.Property(e => e.Id).ValueGeneratedNever();

        	//   builder.Property(e => e.IsDeleted).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Id).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.AspNetUserId).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Notes).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.Name).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

