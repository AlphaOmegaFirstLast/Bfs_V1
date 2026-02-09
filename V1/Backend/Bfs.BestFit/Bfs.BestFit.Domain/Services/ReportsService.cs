using Bfs.Core.Helpers;

using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Domain.Interfaces;
using Bfs.BestFit.Domain.Mapper;

namespace Bfs.BestFit.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IStructureReportReport _structureReportReport;

        private readonly IDataType1Report _dataType1Report;

//Template_Component_AddDeclareEntry
        public ReportsService(
              IStructureReportReport structureReportReport

              ,IDataType1Report dataType1Report

//Template_Component_AddParameterEntry
                            )
        {
              _structureReportReport = structureReportReport;

              _dataType1Report = dataType1Report;

//Template_Component_AddInitEntry
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<StructureReportItem>> StructureReportReportAsync(Bfs.Core.Contracts.QueryRequest<StructureReportFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<StructureReportFilter>, Bfs.Core.Data.QueryRequest<Data.StructureReportFilter>>(contractRequest);

            var entityResult = await _structureReportReport.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.StructureReportItem>, Bfs.Core.Contracts.QueryResponse<StructureReportItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<StructureReportItem> { Items = new List<StructureReportItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<DataType1Item>> DataType1ReportAsync(Bfs.Core.Contracts.QueryRequest<DataType1Filter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<DataType1Filter>, Bfs.Core.Data.QueryRequest<Data.DataType1Filter>>(contractRequest);

            var entityResult = await _dataType1Report.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.DataType1Item>, Bfs.Core.Contracts.QueryResponse<DataType1Item>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<DataType1Item> { Items = new List<DataType1Item>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
    }
}

