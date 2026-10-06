using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;

using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public MasterDbContext _context {get;}
//Template_Field_ChildrenMatrix_AddDeclareEntry

//Template_Start_Code_DontOverwrite_2
public IBfsComponentRepository ComponentRepo { get; set; }
    public IBfsFieldRepository FieldRepo { get; set; }
    public IDeploymentAzureRepository DeploymentAzureRepo { get; set; }
    public IDeploymentLocalRepository DeploymentLocalRepo { get; set; }
//Template_End_Code_DontOverwrite_2

    public UnitOfWork(MasterDbContext dbContext, IScopeData scopeData
//Template_Field_ChildrenMatrix_AddParameterEntry

//Template_Start_Code_DontOverwrite_3
, IBfsComponentRepository componentRepo
        , IBfsFieldRepository fieldRepo
        , IDeploymentAzureRepository deploymentAzureRepo
        , IDeploymentLocalRepository deploymentLocalRepo
//Template_End_Code_DontOverwrite_3

    )
    {
        _context = dbContext;
//Template_Field_ChildrenMatrix_AddInitEntry

//Template_Start_Code_DontOverwrite_4
_context = dbContext;
        ComponentRepo = componentRepo;
        FieldRepo = fieldRepo;
        DeploymentAzureRepo = deploymentAzureRepo;
        DeploymentLocalRepo = deploymentLocalRepo;
//Template_End_Code_DontOverwrite_4

    }

 //Template_Field_ChildrenMatrix_AddUnitOfWorkEntry

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

}
