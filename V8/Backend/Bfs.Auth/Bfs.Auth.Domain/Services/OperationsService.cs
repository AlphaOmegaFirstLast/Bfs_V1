using Bfs.Core.Helpers;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Repositories;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    public OperationsService(IUnitOfWork unitOfwork
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
)
    {
        _unitOfwork = unitOfwork;      
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4        
    }

    public async Task<List<RoleComponentSystemAction>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemAction> matrix)
    {
        var matrixEntity = matrix.ToEntity();
        var entityList = await _unitOfwork.UpdateRoleComponentSystemActionMatrixAsync(parentId, matrixEntity);
        return entityList.ToContract();
    }
//Template_Field_ChildrenMatrix_AddServiceEntry    
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
}

