using Bfs.Core.Helpers;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Repositories;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Services;

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

    //Template_Field_ChildrenMatrix_AddServiceEntry    
    //Template_Start_Code_DontOverwrite_5
    public async Task<SspTransaction?> RolloutSspTransactionAsync(SspTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.RolloutSspTransactionAsync(entity)
            .ConfigureAwait(false);
        var result = await _unitOfwork.SspTransactionRepo.GetAsync(newEntity.Id)
            .ConfigureAwait(false);
        return result?.ToContract();
    }

    public async Task<CashTransaction?> RolloutCashTransactionAsync(CashTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.RolloutCashTransactionAsync(entity);
        var result = await _unitOfwork.CashTransactionRepo.GetAsync(newEntity.Id);
        return result?.ToContract();
    }

    public async Task RollbackSspTransactionAsync(long id)
    {
        await _unitOfwork.RollbackSspTransactionAsync(id);
    }

    public async Task RollbackCashTransactionAsync(long id)
    {
        await _unitOfwork.RollbackCashTransactionAsync(id);
    }

    public async Task<SspTransaction?> ChangeSspTransactionAsync(SspTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.ChangeSspTransactionAsync(entity);
        var result = await _unitOfwork.SspTransactionRepo.GetAsync(newEntity.Id);
        return result?.ToContract();
    }

    public async Task<CashTransaction?> ChangeCashTransactionAsync(CashTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.ChangeCashTransactionAsync(entity);
        var result = await _unitOfwork.CashTransactionRepo.GetAsync(newEntity.Id);
        return result?.ToContract();
    }
    //Template_End_Code_DontOverwrite_5

}

