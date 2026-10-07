using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserRequestStatusService: ICrudService<UserRequestStatus>
    {
        Task<UserRequestStatus> UploadAsync(UserRequestStatus contract);

        Task<QueryResponse<UserRequestStatusListItem>> ListAsync(QueryRequest<UserRequestStatusListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
