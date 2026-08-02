using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Data;

public class AuthDbContext : DbContext
{
 public DbSet<RoleComponentSystemActionEntity> RoleComponentSystemActions { get; set; }
 public DbSet<UserEntity> Users { get; set; }
 public DbSet<AppEntity> Apps { get; set; }
 public DbSet<RoleEntity> Roles { get; set; }
 public DbSet<RoleAppEntity> RoleApps { get; set; }
 public DbSet<RoleUserEntity> RoleUsers { get; set; }
 public DbSet<UserRequestEntity> UserRequests { get; set; }
 public DbSet<UserRequestStatusEntity> UserRequestStatuss { get; set; }
 public DbSet<ResourceRuleEntity> ResourceRules { get; set; }
//Template_Component_RegisterDbSet

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
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
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);

        //Template_ConfigField_CustomFieldList
        //Template_ConfigField_Object
    }
}
