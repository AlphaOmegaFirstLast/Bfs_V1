namespace Bfs.Core.ObjectFields
{
    public static class CustomFieldMapper
    {
        public static CustomField ToContract(this CustomField entity)
        {
            var contract = new CustomField()
            {
                CustomFieldDefinitionId = entity.CustomFieldDefinitionId,
                Name = entity.Name,
                Value = entity.Value,
            };
            return contract;
        }

        public static List<CustomField> ToContract(this IEnumerable<CustomField> CustomFields)
        {
            return CustomFields.Select(x => x.ToContract()).ToList();
        }

        public static List<CustomField> ToEntity(this IEnumerable<CustomField> CustomFields)
        {
            return CustomFields.Select(x => x.ToEntity()).ToList();
        }

        public static CustomField ToEntity(this CustomField contract)
        {
            var entity = new CustomField()
            {
                CustomFieldDefinitionId = contract.CustomFieldDefinitionId,
                Name = contract.Name,
                Value = contract.Value,
            };
            return entity;
        }     
    }
}
