using Bfs.Core.Helpers;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Domain.Mapper;

namespace Bfs.Master.Domain.Services
{
    public class BfsTenantSystemService : IBfsTenantSystemService
    {
        private readonly IBfsTenantSystemRepository _repo;
        private readonly IBfsTenantSystemList _list;
        public BfsTenantSystemService(IBfsTenantSystemRepository repo, IBfsTenantSystemList list)
        {
            _repo = repo;
            _list = list;
        }

        public async Task<BfsTenantSystem?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<BfsTenantSystem>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<BfsTenantSystem> CreateAsync(BfsTenantSystem contract)

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

        public async Task<BfsTenantSystem?> UpdateAsync(BfsTenantSystem contract)
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

        public async Task<BfsTenantSystem> UploadAsync(BfsTenantSystem contract)
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

        public async Task<Bfs.Core.Contracts.QueryResponse<BfsTenantSystemListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<BfsTenantSystemListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<BfsTenantSystemListFilter>, Bfs.Core.Data.QueryRequest<Data.BfsTenantSystemListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.BfsTenantSystemListItem>, Bfs.Core.Contracts.QueryResponse<BfsTenantSystemListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<BfsTenantSystemListItem> { Items = new List<BfsTenantSystemListItem>(), TotalItems = 0, TotalPages = 0 };
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

