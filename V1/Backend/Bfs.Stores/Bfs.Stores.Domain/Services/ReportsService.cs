using Bfs.Core.Helpers;

using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Domain.Interfaces;
using Bfs.Stores.Domain.Mapper;

namespace Bfs.Stores.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IProductTransactionCompare _productTransactionCompare;

//Template_Component_AddDeclareEntry
        public ReportsService(
              IProductTransactionCompare productTransactionCompare

//Template_Component_AddParameterEntry
                            )
        {
              _productTransactionCompare = productTransactionCompare;

//Template_Component_AddInitEntry
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<ProductTransactionCompareItem>> ProductTransactionCompareAsync(Bfs.Core.Contracts.QueryRequest<ProductTransactionCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<ProductTransactionCompareFilter>, Bfs.Core.Data.QueryRequest<Data.ProductTransactionCompareFilter>>(contractRequest);

            var entityResult = await _productTransactionCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.ProductTransactionCompareItem>, Bfs.Core.Contracts.QueryResponse<ProductTransactionCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<ProductTransactionCompareItem> { Items = new List<ProductTransactionCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
    }
}

