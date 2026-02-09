namespace Bfs.Core.ObjectFields
{
    public static class ToolTipInfoMapper
    {
        public static ToolTipInfo ToContract(this ToolTipInfo entity)
        {
            var contract = new ToolTipInfo()
            {
               Icon= entity.Icon,
                ActionLocationId = entity.ActionLocationId,
                Note = entity.Note,
            };
            return contract;
        }

        public static List<ToolTipInfo> ToContract(this IEnumerable<ToolTipInfo> ToolTipInfos)
        {
            return ToolTipInfos.Select(x => x.ToContract()).ToList();
        }

        public static ToolTipInfo ToEntity(this ToolTipInfo contract)
        {
            var entity = new ToolTipInfo()
            {
                Icon = contract.Icon,
                ActionLocationId = contract.ActionLocationId,
                Note = contract.Note,
            };
            return entity;
        }     
    }
}
