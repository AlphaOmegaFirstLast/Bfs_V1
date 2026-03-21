using Bfs.Core.Helpers;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data.Repositories;
using Bfs.Stores.Domain.Interfaces;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain.Mapper;

namespace Bfs.Stores.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;

    public OperationsService(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;       
    }

//Template_Field_ChildrenMatrix_AddServiceEntry    
}

