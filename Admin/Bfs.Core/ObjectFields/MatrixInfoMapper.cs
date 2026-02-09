namespace Bfs.Core.ObjectFields
{
    public static class MatrixInfoMapper
    {
        public static MatrixInfo ToContract(this MatrixInfo entity)
        {
            var contract = new MatrixInfo()
            {
               ParentApi= entity.ParentApi,
                HorizontalApi = entity.HorizontalApi,
                VerticalApi = entity.VerticalApi,
            };
            return contract;
        }

        public static List<MatrixInfo> ToContract(this IEnumerable<MatrixInfo> MatrixInfos)
        {
            return MatrixInfos.Select(x => x.ToContract()).ToList();
        }

        public static MatrixInfo ToEntity(this MatrixInfo contract)
        {
            var entity = new MatrixInfo()
            {
                ParentApi = contract.ParentApi,
                HorizontalApi = contract.HorizontalApi,
                VerticalApi = contract.VerticalApi,
            };
            return entity;
        }     
    }
}
