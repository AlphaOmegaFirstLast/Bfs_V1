using Bfs.Core.Helpers;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Mapper;

namespace Bfs.StockEx.Domain.Services
{
    public class OverdraftPortfolioService : IOverdraftPortfolioService
    {
        private readonly IOverdraftPortfolioRepository _repo;
        private readonly IOverdraftPortfolioList _list;
        public OverdraftPortfolioService(IOverdraftPortfolioRepository repo, IOverdraftPortfolioList list)
        {
            _repo = repo;
            _list = list;
        }

        public async Task<OverdraftPortfolio?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<OverdraftPortfolio>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<OverdraftPortfolio> CreateAsync(OverdraftPortfolio contract)

        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.CreateAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new OverdraftPortfolioCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<OverdraftPortfolio?> UpdateAsync(OverdraftPortfolio contract)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);

            var updatedEntity = contract.ToEntity(existingEntity);

            //var message = new OverdraftPortfolioUpdatedMessage
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

            //var message = new OverdraftPortfolioDeletedMessage
            //{
            //    Entity = PrepareForMessage(existingContract),
            //    CostCenterHierarchyIds = existingContract.CostCenter?.HierarchyIds
            //};

            //await _messagePublisher.PublishMessageAsync(message)
            //    .ConfigureAwait(false);
        }

        public async Task<OverdraftPortfolio> UploadAsync(OverdraftPortfolio contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            //var message = new OverdraftPortfolioCreatedMessage
            //{
            //    Entity = PrepareForMessage(result),
            //};
            //await _messagePublisher.PublishMessageAsync(message).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<OverdraftPortfolioListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<OverdraftPortfolioListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<OverdraftPortfolioListFilter>, Bfs.Core.Data.QueryRequest<Data.OverdraftPortfolioListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.OverdraftPortfolioListItem>, Bfs.Core.Contracts.QueryResponse<OverdraftPortfolioListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<OverdraftPortfolioListItem> { Items = new List<OverdraftPortfolioListItem>(), TotalItems = 0, TotalPages = 0 };
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

