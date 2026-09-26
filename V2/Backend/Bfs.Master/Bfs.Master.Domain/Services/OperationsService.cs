using Bfs.Core.Helpers;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Repositories;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Contracts;
using Bfs.Master.Domain.Mapper;

namespace Bfs.Master.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;

    public OperationsService(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;       
    }

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

