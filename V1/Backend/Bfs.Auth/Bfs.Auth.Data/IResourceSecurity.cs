using Dapper;

namespace Bfs.Auth.Data
{
    public interface IResourceSecurity
    {
        List<RoleResource> RoleResourceList { get; set; }

        string AddSecurityJoin(string queryJoinStatment);
        DynamicParameters AddSecurityParameter(string queryWhereStatment, DynamicParameters parameters);
        string AddSecurityWhere(string queryJoinStatment, string queryWhereStatment);
        void Apply(ref string queryJoinStatment, ref string queryWhereStatment, ref DynamicParameters parameters);
        bool CheckSecurity<T>(string componentName, T entity) where T : class;
    }
}