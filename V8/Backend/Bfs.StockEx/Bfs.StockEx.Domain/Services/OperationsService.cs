using Bfs.Core.Helpers;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Repositories;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain.Mapper;

namespace Bfs.StockEx.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;

    public OperationsService(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;       
    }

//Template_Field_ChildrenMatrix_AddServiceEntry    
}

