namespace Bfs.Core.ObjectFields
{
    public static class ReportInfoMapper
    {
        public static ReportInfo ToContract(this ReportInfo entity)
        {
            var contract = new ReportInfo()
            {
                ParentTable = entity.ParentTable,
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
                IsJoinField = contract.IsJoinField,
                AggregateTypeId = contract.AggregateTypeId,
                ChartElementId = contract.ChartElementId,
            };

            return entity;
        }     
    }
}
