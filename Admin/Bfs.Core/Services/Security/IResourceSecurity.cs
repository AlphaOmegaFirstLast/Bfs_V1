using Bfs.Core.Data;
using Dapper;

namespace Bfs.Core.Services.Security
{
    public interface IResourceSecurity
    {
        bool ApplySecuritySelect(QueryField queryField);
        string AddSecurityJoin(string queryJoinStatment);
        DynamicParameters AddSecurityParameter(string queryWhereStatment, DynamicParameters parameters);
        string AddSecurityWhere(string queryJoinStatment, string queryWhereStatment);
        bool IsApplicableToCRUD<T>(string componentName, T entity) where T : class;
        bool IsApplicableToQuery(List<QueryField> fieldList);
        Task SetRoleResourceAsync();
    }
}