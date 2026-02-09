using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Repositories;
using Bfs.BestFit.Domain.Interfaces;
using Bfs.BestFit.Domain.Mapper;
using Bfs.Core.Helpers;
using Bfs.Core.Services.Deployment;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;

    //Template_Start_Code_DontOverwrite_1
    private readonly IComponentRepository _componentRepo;
    private readonly ITableFieldRepository _tableFieldRepo;
    private readonly IDeploymentAzureStagingRepository _deploymentAzureStagingRepo;
    //Template_Start_Code_DontOverwrite_1

    public OperationsService(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
        //Template_Start_Code_DontOverwrite_2
        _componentRepo = _unitOfwork.ComponentRepo;
        _tableFieldRepo = _unitOfwork.TableFieldRepo;
        _deploymentAzureStagingRepo = _unitOfwork.DeploymentAzureStagingRepo;
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

        var tableFields = await _tableFieldRepo.GetByComponentIdAsync(componentId);
        foreach (var tableField in tableFields)
        {
            var newTableField = tableField.ToContract().ToEntity();
            await _tableFieldRepo.CreateAsync(newTableField);
            newTableField.ComponentId = newComponent.Id;
        }

        await _unitOfwork._context.SaveChangesAsync();
        return newComponent.Id;
    }

    public async Task DeleteComponentTreeAsync(long componentId)
    {
        var component = await _componentRepo.GetAsync(componentId);
        if (component == null)
            throw new Exception($"Component with Id {componentId} not found.");

        await _tableFieldRepo.DeleteByComponentIdAsync(componentId);
        await _componentRepo.DeleteAsync(component);
        await _unitOfwork._context.SaveChangesAsync();
    }

    public async Task DeployToAzureStaging(long id)
    {
        var deployment = await _deploymentAzureStagingRepo.GetAsync(id);
        if (deployment == null)
        {
            throw new ApplicationException($"Staging Deployment Settings not found for id ={id}");
        }
        else
        {
            var azureApiDeployment = new AzureApiDeployment(deployment);
            azureApiDeployment.DoDeploy();
        }
    }

    //Template_End_Code_DontOverwrite_3

    public async Task<List<ComponentSystemAction>> UpdateComponentSystemActionMatrixAsync(long parentId, List<ComponentSystemAction> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateComponentSystemActionMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
    public async Task<List<ComponentBusinessAction>> UpdateComponentBusinessActionMatrixAsync(long parentId, List<ComponentBusinessAction> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateComponentBusinessActionMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
//Template_Field_ChildrenMatrix_AddServiceEntry    
}

