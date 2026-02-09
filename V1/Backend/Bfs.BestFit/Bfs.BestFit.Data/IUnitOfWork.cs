using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Data.Repositories;

public interface IUnitOfWork
{

    //Template_Start_Code_DontOverwrite_1
    BestFitDbContext _context { get; set; }

    IComponentRepository ComponentRepo { get; set; }
    ITableFieldRepository TableFieldRepo { get; set; }
    IDeploymentAzureStagingRepository DeploymentAzureStagingRepo { get; set; }

    //Template_End_Code_DontOverwrite_1

    Task<List<ComponentSystemActionEntity>> UpdateComponentSystemActionMatrixAsync(long parentId, List<ComponentSystemActionEntity> matrix);
    Task<List<ComponentBusinessActionEntity>> UpdateComponentBusinessActionMatrixAsync(long parentId, List<ComponentBusinessActionEntity> matrix);

    //Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
}
