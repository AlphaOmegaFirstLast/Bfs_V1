namespace Bfs.Core.ObjectFields
{
    public static class ReportInfoMapper
    {
        public static ReportInfo ToContract(this ReportInfo entity)
        {
            var contract = new ReportInfo()
            {
                ParentTable = entity.ParentTable,
                IsQueryColumn = entity.IsQueryColumn,
                IsColumnVisible = entity.IsColumnVisible,
                IsJoinField = entity.IsJoinField,
                AggregateTypeId = entity.AggregateTypeId,
                ChartElementId = entity.ChartElementId,
            };
            return contract;
        }

        public static List<ReportInfo> ToContract(this IEnumerable<ReportInfo> ReportInfos)
        {
            return ReportInfos.Select(x => x.ToContract()).ToList();
        }

        public static ReportInfo ToEntity(this ReportInfo contract)
        {
            var entity = new ReportInfo()
            {
                ParentTable = contract.ParentTable,
                IsQueryColumn = contract.IsQueryColumn,
                IsColumnVisible = contract.IsColumnVisible,
                IsJoinField = contract.IsJoinField,
                AggregateTypeId = contract.AggregateTypeId,
                ChartElementId = contract.ChartElementId,
            };

            return entity;
        }     
    }
}
