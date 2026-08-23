using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICurrencyService: ICrudService<Currency>
    {
        Task<Currency> UploadAsync(Currency contract);

        Task<QueryResponse<CurrencyListItem>> ListAsync(QueryRequest<CurrencyListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

