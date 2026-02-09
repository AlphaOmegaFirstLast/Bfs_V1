namespace Bfs.Core.ObjectFields
{
    public static class FieldValidationMapper
    {
        public static FieldValidation ToContract(this FieldValidation entity)
        {
            var contract = new FieldValidation()
            {
               IsRequired= entity.IsRequired,
                MinLength = entity.MinLength,
                MaxLength = entity.MaxLength,
                MinValue = entity.MinValue,
               MaxValue= entity.MaxValue,
               RegexPattern= entity.RegexPattern,
               AllowedValues= entity.AllowedValues,
            };
            return contract;
        }

        public static List<FieldValidation> ToContract(this IEnumerable<FieldValidation> FieldValidations)
        {
            return FieldValidations.Select(x => x.ToContract()).ToList();
        }

        public static FieldValidation ToEntity(this FieldValidation contract)
        {
            var entity = new FieldValidation()
            {
                IsRequired = contract.IsRequired,
                MinLength = contract.MinLength,
                MaxLength = contract.MaxLength,
                MinValue = contract.MinValue,
                MaxValue = contract.MaxValue,
                RegexPattern = contract.RegexPattern,
                AllowedValues = contract.AllowedValues,
            };
            return entity;
        }     
    }
}
