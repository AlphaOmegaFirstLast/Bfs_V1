using Bfs.Core.Helpers;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Services
{
    public class DeploymentAzureService : IDeploymentAzureService
    {
        private readonly IDeploymentAzureRepository _repo;
        private readonly IDeploymentAzureList _list;
   //Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public DeploymentAzureService(
          IDeploymentAzureRepository repo
        , IDeploymentAzureList list
  //Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

        public async Task<DeploymentAzure?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<DeploymentAzure>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<DeploymentAzure> CreateAsync(DeploymentAzure contract)

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

        public async Task<DeploymentAzure?> UpdateAsync(DeploymentAzure contract)
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

        public async Task<DeploymentAzure> UploadAsync(DeploymentAzure contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<DeploymentAzureListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<DeploymentAzureListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<DeploymentAzureListFilter>, Bfs.Core.Data.QueryRequest<Data.DeploymentAzureListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.DeploymentAzureListItem>, Bfs.Core.Contracts.QueryResponse<DeploymentAzureListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<DeploymentAzureListItem> { Items = new List<DeploymentAzureListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

    }
}

