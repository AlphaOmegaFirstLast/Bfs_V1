using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICreditInterestService: ICrudService<CreditInterest>
    {
        Task<CreditInterest> UploadAsync(CreditInterest contract);

        Task<QueryResponse<CreditInterestListItem>> ListAsync(QueryRequest<CreditInterestListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
