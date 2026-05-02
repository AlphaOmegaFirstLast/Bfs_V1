using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bfs.Auth.Data.Configuration
{
    public class UserRequestEntityConfiguration : IEntityTypeConfiguration<UserRequestEntity>
    {
        public static readonly string TableNameCapital = "athUserRequest";

        public void Configure(EntityTypeBuilder<UserRequestEntity> builder)
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
//   builder.Property(e => e.Email).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.UserId).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.RequestDate).HasMaxLength([FieldLength]).IsRequired();
//   builder.Property(e => e.ResponseDate).HasMaxLength([FieldLength]).IsRequired();

        }
    }
}

