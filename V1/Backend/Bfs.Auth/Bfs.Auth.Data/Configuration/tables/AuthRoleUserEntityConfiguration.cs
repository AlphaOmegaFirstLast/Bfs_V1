using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Auth.Data.Configuration
{
    public class AuthRoleUserEntityConfiguration : IEntityTypeConfiguration<AuthRoleUserEntity>
    {
        public static readonly string TableNameCapital = "AuthRoleUser";

        public void Configure(EntityTypeBuilder<AuthRoleUserEntity> builder)
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
