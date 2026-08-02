using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserRequestService: ICrudService<UserRequest>
    {
        Task<UserRequest> UploadAsync(UserRequest contract);

        Task<QueryResponse<UserRequestListItem>> ListAsync(QueryRequest<UserRequestListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

