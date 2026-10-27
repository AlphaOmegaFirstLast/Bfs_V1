using Bfs.Core.Helpers;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Services
{
    public class ProcessStatusService : IProcessStatusService
    {
        private readonly IProcessStatusRepository _repo;
        private readonly IProcessStatusList _list;
   //Template_Start_Code_DontOverwrite_2
   //Template_End_Code_DontOverwrite_2

        public ProcessStatusService(
          IProcessStatusRepository repo
        , IProcessStatusList list
  //Template_Start_Code_DontOverwrite_3
  //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
  //Template_End_Code_DontOverwrite_4
        }

        public async Task<ProcessStatus?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<ProcessStatus>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<ProcessStatus> CreateAsync(ProcessStatus contract, bool isCommit=true)

        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.CreateAsync(entity).ConfigureAwait(false);

            if (isCommit)
            {
                await _repo.SaveAsync().ConfigureAwait(false);

                var result =  await GetAsync(newEntity.Id).ConfigureAwait(false);
                return result;
            }
            else
            {
                return newEntity.ToContract();
            }
        }

        public async Task<ProcessStatus?> UpdateAsync(ProcessStatus contract, bool isCommit = true)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);
            var updatedEntity = contract.ToEntity(existingEntity);

            await _repo.UpdateAsync(updatedEntity).ConfigureAwait(false);
            if (isCommit)
               await _repo.SaveAsync().ConfigureAwait(false);

            return updatedEntity?.ToContract();
        }

        public async Task DeleteAsync(long id, bool isCommit = true)
        {
            var existingEntity = await _repo.GetAsync(id).ConfigureAwait(false);

            await _repo.DeleteAsync(existingEntity).ConfigureAwait(false);

            if (isCommit)
                await _repo.SaveAsync().ConfigureAwait(false);

        }

        public async Task<ProcessStatus> UploadAsync(ProcessStatus contract, bool isCommit = true)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            if (isCommit)
            {
                await _repo.SaveAsync().ConfigureAwait(false);

                var result = await GetAsync(newEntity.Id).ConfigureAwait(false);
                return result;
            }
            else
            {
                return newEntity.ToContract();
            }
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<ProcessStatusListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<ProcessStatusListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<ProcessStatusListFilter>, Bfs.Core.Data.QueryRequest<Data.ProcessStatusListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.ProcessStatusListItem>, Bfs.Core.Contracts.QueryResponse<ProcessStatusListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<ProcessStatusListItem> { Items = new List<ProcessStatusListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
  //Template_End_Code_DontOverwrite_5
    }
}

