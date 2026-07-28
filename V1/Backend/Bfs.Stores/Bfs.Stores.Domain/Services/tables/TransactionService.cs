using Bfs.Core.Helpers;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Domain.Interfaces;
using Bfs.Stores.Domain.Mapper;

namespace Bfs.Stores.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly ITransactionList _list;
        public TransactionService(ITransactionRepository repo, ITransactionList list)
        {
            _repo = repo;
            _list = list;
        }

        public async Task<Transaction?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<Transaction>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<Transaction> CreateAsync(Transaction contract)

        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.CreateAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new TransactionCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<Transaction?> UpdateAsync(Transaction contract)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);

            var updatedEntity = contract.ToEntity(existingEntity);

            //var message = new TransactionUpdatedMessage
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

            //var message = new TransactionDeletedMessage
            //{
            //    Entity = PrepareForMessage(existingContract),
            //    CostCenterHierarchyIds = existingContract.CostCenter?.HierarchyIds
            //};

            //await _messagePublisher.PublishMessageAsync(message)
            //    .ConfigureAwait(false);
        }

        public async Task<Transaction> UploadAsync(Transaction contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new TransactionCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<TransactionListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<TransactionListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<TransactionListFilter>, Bfs.Core.Data.QueryRequest<Data.TransactionListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.TransactionListItem>, Bfs.Core.Contracts.QueryResponse<TransactionListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<TransactionListItem> { Items = new List<TransactionListItem>(), TotalItems = 0, TotalPages = 0 };
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

