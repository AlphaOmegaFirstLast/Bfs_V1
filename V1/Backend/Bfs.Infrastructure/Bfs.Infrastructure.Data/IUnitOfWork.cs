using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Data.Repositories;

public interface IUnitOfWork
{
    //Template_Start_Code_DontOverwrite_1
    InfrastructureDbContext _context { get; set; }

    IBfsComponentRepository ComponentRepo { get; set; }
    IBfsFieldRepository FieldRepo { get; set; }
    IDeploymentAzureRepository DeploymentAzureRepo { get; set; }
    IDeploymentLocalRepository DeploymentLocalRepo { get; set; }

    //Template_End_Code_DontOverwrite_1

    Task<List<BfsComponentSystemActionEntity>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemActionEntity> matrix);
    Task<List<BfsComponentBusinessActionEntity>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessActionEntity> matrix);
    Task<List<BfsClientSystemEntity>> UpdateBfsClientSystemMatrixAsync(long parentId, List<BfsClientSystemEntity> matrix);
Task<List<BfsTenantSystemEntity>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystemEntity> matrix);
//Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
}
