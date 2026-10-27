using Bfs.Core.Helpers;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Services
{
    public class CouponProcessService : ICouponProcessService
    {
        private readonly ICouponProcessRepository _repo;
        private readonly ICouponProcessList _list;
   //Template_Start_Code_DontOverwrite_2
   //Template_End_Code_DontOverwrite_2

        public CouponProcessService(
          ICouponProcessRepository repo
        , ICouponProcessList list
  //Template_Start_Code_DontOverwrite_3
  //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
  //Template_Start_Code_DontOverwrite_4
  //Template_End_Code_DontOverwrite_4
        }

        public async Task<CouponProcess?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<CouponProcess>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<CouponProcess> CreateAsync(CouponProcess contract, bool isCommit=true)

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

        public async Task<CouponProcess?> UpdateAsync(CouponProcess contract, bool isCommit = true)
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

        public async Task<CouponProcess> UploadAsync(CouponProcess contract, bool isCommit = true)
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

        public async Task<Bfs.Core.Contracts.QueryResponse<CouponProcessListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<CouponProcessListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<CouponProcessListFilter>, Bfs.Core.Data.QueryRequest<Data.CouponProcessListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.CouponProcessListItem>, Bfs.Core.Contracts.QueryResponse<CouponProcessListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<CouponProcessListItem> { Items = new List<CouponProcessListItem>(), TotalItems = 0, TotalPages = 0 };
        }

  //Template_Start_Code_DontOverwrite_5
  //Template_End_Code_DontOverwrite_5
    }
}

