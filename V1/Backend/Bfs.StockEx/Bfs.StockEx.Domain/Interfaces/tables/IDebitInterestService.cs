using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IDebitInterestService: ICrudService<DebitInterest>
    {
        Task<DebitInterest> UploadAsync(DebitInterest contract);

        Task<QueryResponse<DebitInterestListItem>> ListAsync(QueryRequest<DebitInterestListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
