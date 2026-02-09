using Bfs.Core.Helpers;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Repositories;
using Bfs.Infrastructure.Domain.Interfaces;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain.Mapper;

namespace Bfs.Infrastructure.Domain.Services;

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
//Template_Field_ChildrenMatrix_AddServiceEntry    
}

