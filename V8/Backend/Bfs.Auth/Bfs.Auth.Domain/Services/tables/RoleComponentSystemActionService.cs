using Bfs.Core.Helpers;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Services
{
    public class RoleComponentSystemActionService : IRoleComponentSystemActionService
    {
        private readonly IRoleComponentSystemActionRepository _repo;
        private readonly IRoleComponentSystemActionList _list;
   //Template_Start_Code_DontOverwrite_2
   //Template_End_Code_DontOverwrite_2

        public RoleComponentSystemActionService(
          IRoleComponentSystemActionRepository repo
        , IRoleComponentSystemActionList list
  //Template_Start_Code_DontOverwrite_3
  //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
  //Template_End_Code_DontOverwrite_4
        }

        public async Task<RoleComponentSystemAction?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<RoleComponentSystemAction>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<RoleComponentSystemAction> CreateAsync(RoleComponentSystemAction contract)

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

        public async Task<RoleComponentSystemAction?> UpdateAsync(RoleComponentSystemAction contract)
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

        public async Task<RoleComponentSystemAction> UploadAsync(RoleComponentSystemAction contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<RoleComponentSystemActionListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<RoleComponentSystemActionListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<RoleComponentSystemActionListFilter>, Bfs.Core.Data.QueryRequest<Data.RoleComponentSystemActionListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.RoleComponentSystemActionListItem>, Bfs.Core.Contracts.QueryResponse<RoleComponentSystemActionListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<RoleComponentSystemActionListItem> { Items = new List<RoleComponentSystemActionListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
  //Template_End_Code_DontOverwrite_5
    }
}

