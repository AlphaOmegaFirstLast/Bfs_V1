using Bfs.Core.Helpers;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Domain.Interfaces;
using Bfs.Infrastructure.Domain.Mapper;

namespace Bfs.Infrastructure.Domain.Services
{
    public class DeploymentLocalService : IDeploymentLocalService
    {
        private readonly IDeploymentLocalRepository _repo;
        private readonly IDeploymentLocalList _list;
        public DeploymentLocalService(IDeploymentLocalRepository repo, IDeploymentLocalList list)
        {
            _repo = repo;
            _list = list;
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

            //var message = new DisplayPageCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<DeploymentLocal?> UpdateAsync(DeploymentLocal contract)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);

            var updatedEntity = contract.ToEntity(existingEntity);

            //var message = new DisplayPageUpdatedMessage
            //{
            //    OldEntity = PrepareForMessage(existingContract),
            //};

            //  existingEntity?.ToEntity();

            await _repo.UpdateAsync(updatedEntity).ConfigureAwait(false);
            await _repo.SaveAsync().ConfigureAwait(false);

            //message.NewEntity = PrepareForMessage(result);
            //await _messagePublisher.PublishMessageAsync(message)
            //    .ConfigureAwait(false);

            return updatedEntity?.ToContract();
        }

        public async Task DeleteAsync(long id)
        {
            var existingEntity = await _repo.GetAsync(id).ConfigureAwait(false);

            //   existingEntity.IsDeleted = true;

            await _repo.DeleteAsync(existingEntity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            //var message = new DisplayPageDeletedMessage
            //{
            //    Entity = PrepareForMessage(existingContract),
            //    CostCenterHierarchyIds = existingContract.CostCenter?.HierarchyIds
            //};

            //await _messagePublisher.PublishMessageAsync(message)
            //    .ConfigureAwait(false);
        }

        public async Task<DeploymentLocal> UploadAsync(DeploymentLocal contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new DisplayPageCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<DeploymentLocalListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<DeploymentLocalListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<DeploymentLocalListFilter>, Bfs.Core.Data.QueryRequest<Data.DeploymentLocalListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.DeploymentLocalListItem>, Bfs.Core.Contracts.QueryResponse<DeploymentLocalListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<DeploymentLocalListItem> { Items = new List<DeploymentLocalListItem>(), TotalItems = 0, TotalPages = 0 };
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

