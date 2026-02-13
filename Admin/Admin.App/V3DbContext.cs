using Admin.App;
using Bfs.Core.ObjectFields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Admin.App
{
    public class V3DbContext : DbContext
    {
        // A DbSet<TEntity> corresponds to a table in the database
        public DbSet<BestFitSystemEntity> BfsSystem { get; set; }
        public DbSet<BestFitComponentEntity> BfsComponent { get; set; }
        public DbSet<BestFitFieldEntity> BfsField { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var BestFitConnection = "Server=localhost;Database=BestFit_V3;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(BestFitConnection);
       
        /* The line below was added to suppress a warning that EF raises whentrying to add-migration for CustomFields.
        "Unable to create a 'DbContext' of type 'AppDbContext'.
        The exception 'An error was generated for warning
        'Microsoft.EntityFrameworkCore.Model.Validation.AccidentalEntityType'
        ** The type 'List<CustomField>' ** has been mapped as an entity type. 
        If you are mapping this type intentionally, then please suppress this warning and report the issue on GitHub.
        This exception can be suppressed or logged by passing event ID 'CoreEventId.AccidentalEntityType' to
        the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'"
         */
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.AccidentalEntityType));
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configure object Field FieldValidation as an owned entity, instruct EF Core to store it as JSON in the database.
            builder.Entity<BestFitFieldEntity>(entity =>
            {
                entity.OwnsOne(e => e.FieldValidation, owned =>
                {
                    owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
                });

                // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
            });
            //Configure object Field ReportInfo as an owned entity, instruct EF Core to store it as JSON in the database.
            builder.Entity<BestFitFieldEntity>(entity =>
            {
                entity.OwnsOne(e => e.ReportInfo, owned =>
                {
                    owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
                });

                // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
            });
            builder.Entity<BestFitFieldEntity>(entity =>
            {
                entity.OwnsOne(e => e.FormInfo, owned =>
                {
                    owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
                });

                // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
            });
            //Configure object Field MatrixInfo as an owned entity, instruct EF Core to store it as JSON in the database.
            builder.Entity<BestFitFieldEntity>(entity =>
            {
                entity.OwnsOne(e => e.MatrixInfo, owned =>
                {
                    owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
                });

                // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
            });
            //Configure object Field ToolTipInfo as an owned entity, instruct EF Core to store it as JSON in the database.
            builder.Entity<BestFitFieldEntity>(entity =>
            {
                entity.OwnsOne(e => e.ToolTipInfo, owned =>
                {
                    owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
                });

                // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
            });
        }
      }
    }