using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Data;

public class StoresDbContext : DbContext
{
 public DbSet<StoreEntity> Stores { get; set; }
 public DbSet<ProductEntity> Products { get; set; }
 public DbSet<TransactionEntity> Transactions { get; set; }
 public DbSet<EffectTypeEntity> EffectTypes { get; set; }
 public DbSet<ThirdPartyTypeEntity> ThirdPartyTypes { get; set; }
 public DbSet<UnitEntity> Units { get; set; }
 public DbSet<CurrencyEntity> Currencys { get; set; }
 public DbSet<OperationEntity> Operations { get; set; }
 public DbSet<AreaEntity> Areas { get; set; }
 public DbSet<DocumentEntity> Documents { get; set; }
 public DbSet<DocumentDetailsEntity> DocumentDetailss { get; set; }
//Template_Component_RegisterDbSet

    public StoresDbContext(DbContextOptions<StoresDbContext> options) : base(options)
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
        builder.ApplyConfigurationsFromAssembly(typeof(StoresDbContext).Assembly);

        //Template_ConfigField_CustomFieldList
        //Template_ConfigField_Object
    }
}

