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
//Template_End_Code_DontOverwrite_5

}

