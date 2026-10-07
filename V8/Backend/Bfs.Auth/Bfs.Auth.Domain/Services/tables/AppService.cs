using Bfs.Core.Helpers;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepository _repo;
        private readonly IAppList _list;
   //Template_Start_Code_DontOverwrite_2
   //Template_End_Code_DontOverwrite_2

        public AppService(
          IAppRepository repo
        , IAppList list
  //Template_Start_Code_DontOverwrite_3
  //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
  //Template_End_Code_DontOverwrite_4
        }

        public async Task<App?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<App>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<App> CreateAsync(App contract)

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

        public async Task<App?> UpdateAsync(App contract)
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

        public async Task<App> UploadAsync(App contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<AppListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<AppListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<AppListFilter>, Bfs.Core.Data.QueryRequest<Data.AppListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.AppListItem>, Bfs.Core.Contracts.QueryResponse<AppListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<AppListItem> { Items = new List<AppListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
  //Template_End_Code_DontOverwrite_5
    }
}

