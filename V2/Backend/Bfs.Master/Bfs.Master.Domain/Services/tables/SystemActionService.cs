using Bfs.Core.Helpers;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Services
{
    public class SystemActionService : ISystemActionService
    {
        private readonly ISystemActionRepository _repo;
        private readonly ISystemActionList _list;
   //Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public SystemActionService(
          ISystemActionRepository repo
        , ISystemActionList list
  //Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

        public async Task<SystemAction?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<SystemAction>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<SystemAction> CreateAsync(SystemAction contract)

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

        public async Task<SystemAction?> UpdateAsync(SystemAction contract)
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

        public async Task<SystemAction> UploadAsync(SystemAction contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<SystemActionListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<SystemActionListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<SystemActionListFilter>, Bfs.Core.Data.QueryRequest<Data.SystemActionListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.SystemActionListItem>, Bfs.Core.Contracts.QueryResponse<SystemActionListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<SystemActionListItem> { Items = new List<SystemActionListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

    }
}

