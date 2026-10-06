using Bfs.Master.Data.Models;
//Template_Start_Code_DontOverwrite_1
using Bfs.Master.Data.Interfaces;
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Repositories;

public interface IUnitOfWork
{
    MasterDbContext _context { get;}
    //Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry

//Template_Start_Code_DontOverwrite_2
IBfsComponentRepository ComponentRepo { get; set; }
    IBfsFieldRepository FieldRepo { get; set; }
    IDeploymentAzureRepository DeploymentAzureRepo { get; set; }
    IDeploymentLocalRepository DeploymentLocalRepo { get; set; }
//Template_End_Code_DontOverwrite_2

}
