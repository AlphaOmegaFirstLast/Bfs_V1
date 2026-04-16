using Bfs.Core.Helpers;
using Bfs.Core.Services.Deployment;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Repositories;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Domain.Mapper;

namespace Bfs.Master.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;

    //Template_Start_Code_DontOverwrite_1
    private readonly IBfsComponentRepository _componentRepo;
    private readonly IBfsFieldRepository _fieldRepo;
    private readonly IDeploymentAzureRepository _deploymentAzureRepo;
    private readonly IDeploymentLocalRepository _deploymentLocalRepo;
    //Template_Start_Code_DontOverwrite_1

    public OperationsService(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        //Template_Start_Code_DontOverwrite_2
        _componentRepo = _unitOfwork.ComponentRepo;
        _fieldRepo = _unitOfwork.FieldRepo;
        _deploymentAzureRepo = _unitOfwork.DeploymentAzureRepo;
        _deploymentLocalRepo = _unitOfwork.DeploymentLocalRepo;
        //Template_Start_Code_DontOverwrite_2

    }

    //Template_Start_Code_DontOverwrite_3

    public async Task<long> DuplicateComponentTreeAsync(long componentId)
    {
        var component = await _componentRepo.GetAsync(componentId);
        if (component == null)
            throw new Exception($"Component with Id {componentId} not found.");

        var newComponent = component.ToContract().ToEntity(); // create a copy
        newComponent.Name = component.Name + " - Copy";

        await _componentRepo.CreateAsync(newComponent); // give the new component a new Id

        var tableFields = await _fieldRepo.GetByComponentIdAsync(componentId);
        foreach (var tableField in tableFields)
        {
            var newTableField = tableField.ToContract().ToEntity();
            await _fieldRepo.CreateAsync(newTableField);
            newTableField.BfsComponentId = newComponent.Id;
        }

        await _unitOfwork._context.SaveChangesAsync();
        return newComponent.Id;
    }

    public async Task DeleteComponentTreeAsync(long componentId)
    {
        var component = await _componentRepo.GetAsync(componentId);
        if (component == null)
            throw new Exception($"Component with Id {componentId} not found.");

        await _fieldRepo.DeleteByComponentIdAsync(componentId);
        await _componentRepo.DeleteAsync(component);
        await _unitOfwork._context.SaveChangesAsync();
    }

    public async Task PublishToLocal(long id)
    {
        var deploymentEntity = await _deploymentLocalRepo.GetAsync(id);
        if (deploymentEntity == null)
        {
            throw new ApplicationException($"Local Deployment Settings not found for id ={id}");
        }
        else
        {
            DeploymentManager.PublishToLocal(deploymentEntity);
        }
    }

    public async Task DeployToLocal(long id)
    {
        var deploymentEntity = await _deploymentLocalRepo.GetAsync(id);
        if (deploymentEntity == null)
        {
            throw new ApplicationException($"Local Deployment Settings not found for id ={id}");
        }
        else
        {
            DeploymentManager.DeployToLocal(deploymentEntity);
        }
    }

    public async Task DeployToAzure(long id)
    {
        var deploymentEntity = await _deploymentAzureRepo.GetAsync(id);
        if (deploymentEntity == null)
        {
            throw new ApplicationException($"Local Deployment Settings not found for id ={id}");
        }
        else
        {
            DeploymentManager.DeployToAzure(deploymentEntity);
        }
    }
    //Template_End_Code_DontOverwrite_3
    public async Task<List<BfsComponentSystemAction>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemAction> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateBfsComponentSystemActionMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
    public async Task<List<BfsComponentBusinessAction>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessAction> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateBfsComponentBusinessActionMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
    public async Task<List<BfsTenantSystem>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystem> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateBfsTenantSystemMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
//Template_Field_ChildrenMatrix_AddServiceEntry    
}

