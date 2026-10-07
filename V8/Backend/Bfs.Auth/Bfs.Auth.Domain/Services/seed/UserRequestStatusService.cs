using Bfs.Core.Helpers;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Services
{
    public class UserRequestStatusService : IUserRequestStatusService
    {
        private readonly IUserRequestStatusRepository _repo;
        private readonly IUserRequestStatusList _list;
   //Template_Start_Code_DontOverwrite_2
   //Template_End_Code_DontOverwrite_2

        public UserRequestStatusService(
          IUserRequestStatusRepository repo
        , IUserRequestStatusList list
  //Template_Start_Code_DontOverwrite_3
  //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
  //Template_End_Code_DontOverwrite_4
        }

        public async Task<UserRequestStatus?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<UserRequestStatus>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<UserRequestStatus> CreateAsync(UserRequestStatus contract)

        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.CreateAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<UserRequestStatus?> UpdateAsync(UserRequestStatus contract)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);
            var updatedEntity = contract.ToEntity(existingEntity);

            await _repo.UpdateAsync(updatedEntity).ConfigureAwait(false);
            await _repo.SaveAsync().ConfigureAwait(false);

            return updatedEntity?.ToContract();
        }

        public async Task DeleteAsync(long id)
        {
            var existingEntity = await _repo.GetAsync(id).ConfigureAwait(false);

            await _repo.DeleteAsync(existingEntity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

        }

        public async Task<UserRequestStatus> UploadAsync(UserRequestStatus contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<UserRequestStatusListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<UserRequestStatusListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<UserRequestStatusListFilter>, Bfs.Core.Data.QueryRequest<Data.UserRequestStatusListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.UserRequestStatusListItem>, Bfs.Core.Contracts.QueryResponse<UserRequestStatusListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<UserRequestStatusListItem> { Items = new List<UserRequestStatusListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
  //Template_End_Code_DontOverwrite_5
    }
}

