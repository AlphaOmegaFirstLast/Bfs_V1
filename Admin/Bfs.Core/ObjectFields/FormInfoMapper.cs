namespace Bfs.Core.ObjectFields
{
    public static class FormInfoMapper
    {
        public static FormInfo ToContract(this FormInfo entity)
        {
            var contract = new FormInfo()
            {
                Column = entity.Column,
                Row = entity.Row,
                FormControlTypeId = entity.FormControlTypeId,
            };
            return contract;
        }

        public static List<FormInfo> ToContract(this IEnumerable<FormInfo> FormInfos)
        {
            return FormInfos.Select(x => x.ToContract()).ToList();
        }

        public static FormInfo ToEntity(this FormInfo contract)
        {
            var entity = new FormInfo()
            {
                Column = contract.Column,
                Row = contract.Row,
                FormControlTypeId = contract.FormControlTypeId,
             };
            return entity;
        }     
    }
}
