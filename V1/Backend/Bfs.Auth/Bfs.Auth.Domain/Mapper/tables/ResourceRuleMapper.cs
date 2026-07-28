using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class ResourceRuleMapper
    {
        public static ResourceRule ToContract(this ResourceRuleEntity entity)
        {
            var contract = new ResourceRule()
            {
               SelectBlackList= entity.SelectBlackList,
IsDeleted= entity.IsDeleted,
Id= entity.Id,
BfsComponentName= entity.BfsComponentName,
JoinStatement= entity.JoinStatement,
WhereStatement= entity.WhereStatement,
ParameterName= entity.ParameterName,
ParameterValue= entity.ParameterValue,
ParameterType= entity.ParameterType,
RoleName= entity.RoleName,

               RoleId= entity.RoleId,
BfsComponentId= entity.BfsComponentId,

            };

            return contract;
        }

        public static List<ResourceRule> ToContract(this IEnumerable<ResourceRuleEntity> ResourceRules)
        {
            return ResourceRules.Select(x => x.ToContract()).ToList();
        }

        public static List<ResourceRuleEntity> ToEntity(this IEnumerable<ResourceRule> ResourceRules)
        {
            return ResourceRules.Select(x => x.ToEntity()).ToList();
        }

        public static ResourceRuleEntity ToEntity(this ResourceRule contract, ResourceRuleEntity entity = null)
        {
            var ResourceRuleEntity = entity ?? new();

            ResourceRuleEntity.SelectBlackList= contract.SelectBlackList;
ResourceRuleEntity.IsDeleted= contract.IsDeleted;
ResourceRuleEntity.Id= contract.Id;
ResourceRuleEntity.BfsComponentName= contract.BfsComponentName;
ResourceRuleEntity.JoinStatement= contract.JoinStatement;
ResourceRuleEntity.WhereStatement= contract.WhereStatement;
ResourceRuleEntity.ParameterName= contract.ParameterName;
ResourceRuleEntity.ParameterValue= contract.ParameterValue;
ResourceRuleEntity.ParameterType= contract.ParameterType;
ResourceRuleEntity.RoleName= contract.RoleName;

            ResourceRuleEntity.RoleId= contract.RoleId;
ResourceRuleEntity.BfsComponentId= contract.BfsComponentId;

            return ResourceRuleEntity;
        }     
    }
}

