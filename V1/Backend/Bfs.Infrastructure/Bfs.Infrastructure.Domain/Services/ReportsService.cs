using Bfs.Core.Helpers;

using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Domain.Interfaces;
using Bfs.Infrastructure.Domain.Mapper;

namespace Bfs.Infrastructure.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IStructureCompare _structureCompare;

//Template_Component_AddDeclareEntry
        public ReportsService(

              IStructureCompare structureCompare

//Template_Component_AddParameterEntry
                            )
        {
              _structureCompare = structureCompare;

//Template_Component_AddInitEntry
        }


        public async Task<Bfs.Core.Contracts.QueryResponse<StructureCompareItem>> StructureCompareAsync(Bfs.Core.Contracts.QueryRequest<StructureCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<StructureCompareFilter>, Bfs.Core.Data.QueryRequest<Data.StructureCompareFilter>>(contractRequest);

            var entityResult = await _structureCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.StructureCompareItem>, Bfs.Core.Contracts.QueryResponse<StructureCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<StructureCompareItem> { Items = new List<StructureCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
    }
}

