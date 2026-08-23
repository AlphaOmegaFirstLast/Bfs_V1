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

    public async Task<SspTransaction?> RolloutTransactionAsync(SspTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.RolloutTransactionAsync(entity)
            .ConfigureAwait(false);
        var result = await _unitOfwork.SspTransactionRepo.GetAsync(newEntity.Id)
            .ConfigureAwait(false);
        return result?.ToContract();
    }

    public async Task<CashTransaction?> RolloutTransactionAsync(CashTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.RolloutTransactionAsync(entity)
            .ConfigureAwait(false);
        var result = await _unitOfwork.CashTransactionRepo.GetAsync(newEntity.Id)
            .ConfigureAwait(false);
        return result?.ToContract();
    }
    //Template_Field_ChildrenMatrix_AddServiceEntry    
}

