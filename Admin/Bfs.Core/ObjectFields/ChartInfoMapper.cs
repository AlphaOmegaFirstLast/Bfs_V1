namespace Bfs.Core.ObjectFields
{
    public static class ChartInfoMapper
    {
        public static ChartInfo ToContract(this ChartInfo entity)
        {
            var contract = new ChartInfo()
            {
                HorizontalField = entity.HorizontalField,
                VerticalField = entity.VerticalField,
            };
            return contract;
        }

        public static List<ChartInfo> ToContract(this IEnumerable<ChartInfo> ChartInfos)
        {
            return ChartInfos.Select(x => x.ToContract()).ToList();
        }

        public static ChartInfo ToEntity(this ChartInfo contract)
        {
            var entity = new ChartInfo()
            {
                HorizontalField = contract.HorizontalField,
                VerticalField = contract.VerticalField,
            };

            return entity;
        }     
    }
}
