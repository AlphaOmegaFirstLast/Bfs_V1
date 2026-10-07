using Bfs.Core.Helpers;

using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
namespace Bfs.Auth.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IRoleRepCompare _roleRepCompare;

//Template_Component_AddDeclareEntry
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
        public ReportsService(
              IRoleRepCompare roleRepCompare

//Template_Component_AddParameterEntry
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
                            )
        {
              _roleRepCompare = roleRepCompare;

//Template_Component_AddInitEntry
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<RoleRepCompareItem>> RoleRepCompareAsync(Bfs.Core.Contracts.QueryRequest<RoleRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<RoleRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.RoleRepCompareFilter>>(contractRequest);

            var entityResult = await _roleRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.RoleRepCompareItem>, Bfs.Core.Contracts.QueryResponse<RoleRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<RoleRepCompareItem> { Items = new List<RoleRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
    }
}

