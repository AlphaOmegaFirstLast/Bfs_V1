using Bfs.Core.Helpers;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Repositories;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain.Mapper;

namespace Bfs.Auth.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;

    public OperationsService(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;       
    }

    public async Task<List<RoleComponentSystemAction>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemAction> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateRoleComponentSystemActionMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
//Template_Field_ChildrenMatrix_AddServiceEntry    
}

