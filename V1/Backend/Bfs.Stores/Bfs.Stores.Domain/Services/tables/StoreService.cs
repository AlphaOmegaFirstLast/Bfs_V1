using Bfs.Core.Helpers;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Domain.Interfaces;
using Bfs.Stores.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Stores.Domain.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repo;
        private readonly IStoreList _list;
   //Template_Start_Code_DontOverwrite_2
   //Template_End_Code_DontOverwrite_2

        public StoreService(
          IStoreRepository repo
        , IStoreList list
  //Template_Start_Code_DontOverwrite_3
  //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
  //Template_End_Code_DontOverwrite_4
        }

        public async Task<Store?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<Store>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<Store> CreateAsync(Store contract)

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

        public async Task<Store?> UpdateAsync(Store contract)
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

        public async Task<Store> UploadAsync(Store contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<StoreListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<StoreListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<StoreListFilter>, Bfs.Core.Data.QueryRequest<Data.StoreListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.StoreListItem>, Bfs.Core.Contracts.QueryResponse<StoreListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<StoreListItem> { Items = new List<StoreListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
  //Template_End_Code_DontOverwrite_5
    }
}

