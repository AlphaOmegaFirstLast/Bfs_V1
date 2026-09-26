using Bfs.Core.Helpers;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Services
{
    public class DeploymentLocalService : IDeploymentLocalService
    {
        private readonly IDeploymentLocalRepository _repo;
        private readonly IDeploymentLocalList _list;
   //Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public DeploymentLocalService(
          IDeploymentLocalRepository repo
        , IDeploymentLocalList list
  //Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

        public async Task<DeploymentLocal?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<DeploymentLocal>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<DeploymentLocal> CreateAsync(DeploymentLocal contract)

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

        public async Task<DeploymentLocal?> UpdateAsync(DeploymentLocal contract)
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

        public async Task<DeploymentLocal> UploadAsync(DeploymentLocal contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<DeploymentLocalListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<DeploymentLocalListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<DeploymentLocalListFilter>, Bfs.Core.Data.QueryRequest<Data.DeploymentLocalListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.DeploymentLocalListItem>, Bfs.Core.Contracts.QueryResponse<DeploymentLocalListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<DeploymentLocalListItem> { Items = new List<DeploymentLocalListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

    }
}

