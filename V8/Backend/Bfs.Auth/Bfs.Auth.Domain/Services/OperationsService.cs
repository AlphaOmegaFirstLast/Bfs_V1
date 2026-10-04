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

//Template_Field_ChildrenMatrix_AddServiceEntry    
}

