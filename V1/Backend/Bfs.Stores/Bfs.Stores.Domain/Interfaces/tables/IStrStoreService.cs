using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IStrStoreService
    {
        Task<StrStore?> GetAsync(long id);
        Task<List<StrStore>> GetAsync();

        Task<StrStore> CreateAsync(StrStore contract);
        Task<StrStore?> UpdateAsync(StrStore contract);
        Task DeleteAsync(long id);
        Task<StrStore> UploadAsync(StrStore contract);

        Task<QueryResponse<StrStoreListItem>> ListAsync(QueryRequest<StrStoreListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
