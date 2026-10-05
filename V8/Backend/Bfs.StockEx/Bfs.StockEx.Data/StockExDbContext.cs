using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Data;

public class StockExDbContext : DbContext
{
//Template_Component_RegisterDbSet
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
    public StockExDbContext(DbContextOptions<StockExDbContext> options) : base(options)
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
        builder.ApplyConfigurationsFromAssembly(typeof(StockExDbContext).Assembly);

        //Template_ConfigField_CustomFieldList
        //Template_ConfigField_Object
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
