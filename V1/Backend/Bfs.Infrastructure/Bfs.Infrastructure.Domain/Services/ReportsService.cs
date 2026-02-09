using Bfs.Core.Helpers;

using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Domain.Interfaces;
using Bfs.Infrastructure.Domain.Mapper;

namespace Bfs.Infrastructure.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IStructureReportReport _structureReportReport;

//Template_Component_AddDeclareEntry
        public ReportsService(
              IStructureReportReport structureReportReport

//Template_Component_AddParameterEntry
                            )
        {
              _structureReportReport = structureReportReport;

//Template_Component_AddInitEntry
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<StructureReportItem>> StructureReportReportAsync(Bfs.Core.Contracts.QueryRequest<StructureReportFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<StructureReportFilter>, Bfs.Core.Data.QueryRequest<Data.StructureReportFilter>>(contractRequest);

            var entityResult = await _structureReportReport.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.StructureReportItem>, Bfs.Core.Contracts.QueryResponse<StructureReportItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<StructureReportItem> { Items = new List<StructureReportItem>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
    }
}

