using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Data;

public class BestFitDbContext : DbContext
{
 public DbSet<SystemTemplateEntity> SystemTemplates { get; set; }
 public DbSet<DataTypeEntity> DataTypes { get; set; }
 public DbSet<FilterTypeEntity> FilterTypes { get; set; }
 public DbSet<FormControlTypeEntity> FormControlTypes { get; set; }
 public DbSet<ComponentTypeEntity> ComponentTypes { get; set; }
 public DbSet<BackendDataTypeEntity> BackendDataTypes { get; set; }
 public DbSet<ActionTypeEntity> ActionTypes { get; set; }
 public DbSet<AggregateTypeEntity> AggregateTypes { get; set; }
 public DbSet<ChartElementEntity> ChartElements { get; set; }
 public DbSet<ActionLocationEntity> ActionLocations { get; set; }
 public DbSet<SystemActionEntity> SystemActions { get; set; }
 public DbSet<ComponentEntity> Components { get; set; }
 public DbSet<TableFieldEntity> TableFields { get; set; }
 public DbSet<SystemInfoEntity> SystemInfos { get; set; }
 public DbSet<ClientEntity> Clients { get; set; }
 public DbSet<CustomReportsEntity> CustomReportss { get; set; }
 public DbSet<CustomFieldDefinitionEntity> CustomFieldDefinitions { get; set; }
 public DbSet<BusinessActionEntity> BusinessActions { get; set; }
 public DbSet<ComponentSystemActionEntity> ComponentSystemActions { get; set; }
 public DbSet<ComponentBusinessActionEntity> ComponentBusinessActions { get; set; }
 public DbSet<DeploymentAzureStagingEntity> DeploymentAzureStagings { get; set; }
 public DbSet<DeploymentLocalEntity> DeploymentLocals { get; set; }

//Template_Component_RegisterDbSet

    public BestFitDbContext(DbContextOptions<BestFitDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { /* The line below was added to suppress a warning that EF raises whentrying to add-migration for CustomFields.
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
        builder.ApplyConfigurationsFromAssembly(typeof(BestFitDbContext).Assembly);

        builder.Entity<ClientEntity>(entity =>
        {
            entity.OwnsMany(e => e.CustomFields, owned =>
            {
                owned.ToJson();   // store the collection as JSON
            });
        });
//Template_ConfigField_CustomFieldList
                //Configure object Field FieldValidation as an owned entity, instruct EF Core to store it as JSON in the database.
        builder.Entity<TableFieldEntity>(entity =>
        {
            entity.OwnsOne(e => e.FieldValidation, owned =>
            {
                owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
            });

            // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
        });
        //Configure object Field ReportInfo as an owned entity, instruct EF Core to store it as JSON in the database.
        builder.Entity<TableFieldEntity>(entity =>
        {
            entity.OwnsOne(e => e.ReportInfo, owned =>
            {
                owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
            });

            // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
        });
        //Configure object Field MatrixInfo as an owned entity, instruct EF Core to store it as JSON in the database.
        builder.Entity<TableFieldEntity>(entity =>
        {
            entity.OwnsOne(e => e.MatrixInfo, owned =>
            {
                owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
            });

            // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
        });
        //Configure object Field ToolTipInfo as an owned entity, instruct EF Core to store it as JSON in the database.
        builder.Entity<TableFieldEntity>(entity =>
        {
            entity.OwnsOne(e => e.ToolTipInfo, owned =>
            {
                owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
            });

            // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
        });
        //Configure object Field FormInfo as an owned entity, instruct EF Core to store it as JSON in the database.
        builder.Entity<TableFieldEntity>(entity =>
        {
            entity.OwnsOne(e => e.FormInfo, owned =>
            {
                owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
            });

            // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
        });
        //Configure object Field FieldValidation as an owned entity, instruct EF Core to store it as JSON in the database.
        builder.Entity<CustomFieldDefinitionEntity>(entity =>
        {
            entity.OwnsOne(e => e.FieldValidation, owned =>
            {
                owned.ToJson();  // EF Core 7.0+ for the .ToJson() method. usually handles nullability correctly by default.
            });

            // Note: If you have other properties/configurations for the Entity, they would also go inside this lambda.
        });

//Template_ConfigField_Object
    }
}
