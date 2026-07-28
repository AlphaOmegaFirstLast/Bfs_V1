using Bfs.Core.Helpers;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Mapper;

namespace Bfs.Auth.Domain.Services
{
    public class ResourceRuleService : IResourceRuleService
    {
        private readonly IResourceRuleRepository _repo;
        private readonly IResourceRuleList _list;
        public ResourceRuleService(IResourceRuleRepository repo, IResourceRuleList list)
        {
            _repo = repo;
            _list = list;
        }

        public async Task<ResourceRule?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<ResourceRule>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<ResourceRule> CreateAsync(ResourceRule contract)

        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.CreateAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new ResourceRuleCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<ResourceRule?> UpdateAsync(ResourceRule contract)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);

            var updatedEntity = contract.ToEntity(existingEntity);

            //var message = new ResourceRuleUpdatedMessage
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

            //var message = new ResourceRuleDeletedMessage
            //{
            //    Entity = PrepareForMessage(existingContract),
            //    CostCenterHierarchyIds = existingContract.CostCenter?.HierarchyIds
            //};

            //await _messagePublisher.PublishMessageAsync(message)
            //    .ConfigureAwait(false);
        }

        public async Task<ResourceRule> UploadAsync(ResourceRule contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new ResourceRuleCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<ResourceRuleListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<ResourceRuleListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<ResourceRuleListFilter>, Bfs.Core.Data.QueryRequest<Data.ResourceRuleListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.ResourceRuleListItem>, Bfs.Core.Contracts.QueryResponse<ResourceRuleListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<ResourceRuleListItem> { Items = new List<ResourceRuleListItem>(), TotalItems = 0, TotalPages = 0 };
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

