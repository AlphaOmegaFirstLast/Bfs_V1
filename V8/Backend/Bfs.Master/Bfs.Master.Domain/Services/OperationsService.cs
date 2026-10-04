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

//Template_Field_ChildrenMatrix_AddServiceEntry    
}

